using Indotalent.Applications.Lots;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Contracts;
using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.PurchaseOrders
{
    public class PurchaseOrderService : Repository<PurchaseOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly LotItemService _lotItemService;
        private readonly LotService _lotService;
        private readonly IServiceProvider _serviceProvider;

        public PurchaseOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            LotItemService lotItemService,
            LotService lotService,
            IServiceProvider serviceProvider) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _lotItemService = lotItemService;
            _lotService = lotService;
            _serviceProvider = serviceProvider;
        }

        public override async Task AddAsync(PurchaseOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO");
            entity.TaxAmount = 0.0m;
            entity.AfterTaxAmount = 0.0m;
            entity.BeforeTaxAmount = 0.0m;

            if (entity.LotId.HasValue)
            {
                var lot = await _lotService.GetByIdAsync(entity.LotId.Value);
                entity.ContainerM3 = lot!.ContainerM3;
                entity.TotalTransportContainerCost = lot!.TotalTransportContainerCost;
                entity.TotalAgencyCost = lot!.TotalAgencyCost;
            }

            await base.AddAsync(entity);
            if (entity.LotId.HasValue)
            {
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

            foreach (var purchaseOrderItem in lotItems.Select(lotItem => new PurchaseOrderItem
            {
                CreatedAtUtc = DateTime.Now,
                ProductId = lotItem.Product!.Id,
                UnitCost = lotItem.UnitCost,
                UnitCostBrazil = lotItem.UnitCostBrazil,
                UnitCostDiscounted = lotItem.UnitCostDiscounted,
                PurchaseOrderId = purchaseOrder.Id,
                Quantity = lotItem.Quantity,
                UnitCostBolivia = 0m,
                Summary = lotItem.Product!.Number,
                ShowOrderItem = true,
                IsAssembly = lotItem.Product!.IsAssembly,
                LotItemId = lotItem.Id
            }))
            {
                await purchaseOrderItemService.AddAsync(purchaseOrderItem);
            }
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
                master.BeforeTaxAmount = 0;
                foreach (var item in children)
                {
                    master.BeforeTaxAmount += item.Total;
                }

                if (master.Tax != null)
                {
                    master.TaxAmount = (master.Tax.Percentage / 100.0m) * master.BeforeTaxAmount;
                }

                master.AfterTaxAmount = master.BeforeTaxAmount + master.TaxAmount;
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
    }
}
