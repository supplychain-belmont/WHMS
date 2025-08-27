using Indotalent.Application.PurchaseOrders;
using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.Lots;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Applications.TransferIns;
using Indotalent.Applications.TransferOuts;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;
using Indotalent.DTOs;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrders
{
    public class PurchaseOrderService : Repository<PurchaseOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderProcessor _purchaseOrderProcessor;
        private readonly LotService _lotService;
        private readonly LotItemService _lotItemService;
        private readonly IServiceProvider _serviceProvider;

        public PurchaseOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderProcessor purchaseOrderProcessor,
            LotService lotService,
            LotItemService lotItemService,
            IServiceProvider serviceProvider) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderProcessor = purchaseOrderProcessor;
            _lotService = lotService;
            _lotItemService = lotItemService;
            _serviceProvider = serviceProvider;
        }

        public override async Task AddAsync(PurchaseOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;

            await base.AddAsync(entity);

            if (entity.LotId.HasValue)
            {
                var lot = await _lotService.GetByIdAsync(entity.LotId.Value);
                _purchaseOrderProcessor.CalculatePurchaseOrder(lot!, entity);
                await CreatePurchaseOrderItemAsync(entity);
            }
        }

        private async Task CreatePurchaseOrderItemAsync(PurchaseOrder purchaseOrder)
        {
            using var scope = _serviceProvider.CreateScope();
            var purchaseOrderItemService = scope.ServiceProvider.GetRequiredService<PurchaseOrderItemService>();
            var lotItems = await _lotItemService.GetAll()
                .Include(li => li.Product)
                .Include(li => li.Lot)
                .Where(li => li.LotId == purchaseOrder.LotId)
                .ToListAsync();

            var orderItems = _purchaseOrderProcessor.CreatePurchaseOrderItems(lotItems, purchaseOrder);
            await purchaseOrderItemService.AddRangeAsync(orderItems);
        }

        public async Task RecalculateParentAsync(int? masterId)
        {
            var master = await _context.Set<PurchaseOrder>()
                .Include(x => x.Tax)
                .Where(x => x.Id == masterId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _context.Set<PurchaseOrderItem>()
                .Where(x => x.PurchaseOrderId == masterId && x.IsNotDeleted && !x.IsAssembly)
                .AsNoTracking()
                .ToListAsync();

            if (master != null)
            {
                _purchaseOrderProcessor.RecalculateParent(master, children);
                _context.Set<PurchaseOrder>().Update(master);
                await _context.SaveChangesAsync();
            }
        }


        public override async Task UpdateAsync(PurchaseOrder? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                _context.Set<PurchaseOrder>().Update(entity);
                await _context.SaveChangesAsync();

                await RecalculateParentAsync(entity.Id);
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }
        public async Task<PurchaseOrder> AutoGeneratePurchaseOrderAsync(AutoPurchaseOrderDto input)
        {
            var po = new PurchaseOrder
            {
                Number = _numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO"),
                OrderDate = DateTime.UtcNow,
                VendorId = input.VendorId,
                TaxId = input.TaxId,
                ContainerM3 = input.ContainerM3,
                LotId = input.LotId,
                OrderStatus = PurchaseOrderStatus.Draft,
                Description = input.Description,
                TaxAmount = 0.0m,
                AfterTaxAmount = 0.0m,
                BeforeTaxAmount = 0.0m
            };

            await base.AddAsync(po);

            if (po.LotId.HasValue)
            {
                var lot = await _lotService.GetByIdAsync(po.LotId.Value);
                _purchaseOrderProcessor.CalculatePurchaseOrder(lot!, po);
                await CreatePurchaseOrderItemAsync(po);
            }
            po.OrderStatus = PurchaseOrderStatus.Confirmed;
            _context.PurchaseOrder.Update(po);
            await _context.SaveChangesAsync();

            return po;
        }
        public async Task<(PurchaseOrder Po, TransferOut To, TransferIn Ti, int ToLines, int TiLines)>
    AutoGenerateWithTransfersAsync(AutoPOTransferFlowDto input)
    {
        if (input == null) throw new Exception("Input is null");
        if (input.WarehouseId <= 0) throw new Exception("WarehouseId is required.");

        using var tx = await _context.Database.BeginTransactionAsync();

        var po = await AutoGeneratePurchaseOrderAsync(new AutoPurchaseOrderDto
        {
            VendorId     = input.VendorId,
            TaxId        = input.TaxId,
            ContainerM3  = input.ContainerM3,
            LotId        = input.LotId,
            Description  = input.Description
        });

        var poItems = await _context.Set<PurchaseOrderItem>()
            .Include(i => i.Product)
            .Where(i => i.PurchaseOrderId == po.Id && i.IsNotDeleted && i.ShowOrderItem)
            .ToListAsync();

        if (poItems.Count == 0)
            throw new Exception("Purchase Order has no items to transfer/receive.");

        using var scope = _serviceProvider.CreateScope();
        var toSvc     = scope.ServiceProvider.GetRequiredService<TransferOutService>();
        var tiSvc     = scope.ServiceProvider.GetRequiredService<TransferInService>();
        var invSvc    = scope.ServiceProvider.GetRequiredService<InventoryTransactionService>();
        var numSvc    = scope.ServiceProvider.GetRequiredService<NumberSequenceService>();
        var whSvc     = scope.ServiceProvider.GetRequiredService<Warehouses.WarehouseService>();

        var vendorWh = whSvc.GetVendorWarehouse();
        if (vendorWh == null) throw new Exception("Vendor warehouse not found.");

        var releaseDate = input.ReleaseDate ?? DateTime.UtcNow;
        var receiveDate = input.ReceiveDate ?? DateTime.UtcNow;

        var to = new TransferOut
        {
            Description         = input.TransferDescription,
            TransferReleaseDate = releaseDate,
            Status              = TransferStatus.Archived,
            WarehouseFromId     = vendorWh.Id,
            WarehouseToId       = input.WarehouseId,
            RowGuid             = Guid.NewGuid(),
            CreatedAtUtc        = DateTime.UtcNow
        };
        await toSvc.AddAsync(to);

        var toChildren = new List<InventoryTransaction>();
        foreach (var it in poItems)
        {
            if (it.Product == null || it.Product.Physical == false) continue;
            var qty = it.Quantity;
            if (qty <= 0m) continue;

            var ivt = new InventoryTransaction
            {
                Number        = numSvc.GenerateNumber(nameof(InventoryTransaction), "", "IVT"),
                ModuleId      = to.Id,
                ModuleName    = nameof(TransferOut),
                ModuleCode    = "TO-OUT",
                ModuleNumber  = to.Number ?? string.Empty,
                ProductId     = it.ProductId,
                Movement      = qty,
                MovementDate  = releaseDate,
                Status        = (InventoryTransactionStatus)to.Status!,
                WarehouseId   = to.WarehouseFromId!.Value,
                CreatedAtUtc  = DateTime.UtcNow
            };
            toChildren.Add(ivt);
        }
        if (toChildren.Count == 0)
            throw new Exception("No transferable items found (physical qty > 0).");

        await invSvc.AddRangeAsync(toChildren);
        var toLines = toChildren.Count;
        var ti = new TransferIn
        {
            Description          = input.TransferDescription ?? $"Auto TI from PO {po.Number}",
            TransferOutId        = to.Id,
            TransferReceiveDate  = receiveDate,
            Status               = TransferStatus.Archived, 
            RowGuid              = Guid.NewGuid(),
            CreatedAtUtc         = DateTime.UtcNow
        };
        await tiSvc.AddAsync(ti);

        var tiLines = await invSvc
            .GetAll()
            .Where(x => x.ModuleId == ti.Id && x.ModuleName == nameof(TransferIn))
            .CountAsync();

        await tx.CommitAsync();

        return (po, to, ti, toLines, tiLines);
    }
    }
}
