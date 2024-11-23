using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;
using Indotalent.Models.Enums;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.GoodsReceives
{
    public class GoodsReceiveService : Repository<GoodsReceive>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly ProductService _productService;

        public GoodsReceiveService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderItemService purchaseOrderItemService,
            InventoryTransactionService inventoryTransactionService,
            ProductService productService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderItemService = purchaseOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
            _productService = productService;
        }

        public override async Task AddAsync(GoodsReceive? entity)
        {
            var moduleName = nameof(GoodsReceive);
            await base.AddAsync(entity);
            var purchaseItems = await _purchaseOrderItemService.GetAll()
                .Include(item => item.Product)
                .Include(item => item.LotItem)
                .ThenInclude(lotItem => lotItem!.Lot)
                .Where(item => item.PurchaseOrderId == entity!.PurchaseOrderId)
                .ToListAsync();

            foreach (PurchaseOrderItem purchaseOrderItem in purchaseItems)
            {
                if (purchaseOrderItem.LotItemId.HasValue)
                {
                    var product = purchaseOrderItem.Product;
                    var lot = purchaseOrderItem.LotItem!.Lot;
                    var productLotItem = product!.Clone();
                    productLotItem.RowGuid = Guid.NewGuid();
                    productLotItem.Id = 0;
                    productLotItem.Name = $"{product.Name}/{lot!.Name}";
                    productLotItem.UnitCost = purchaseOrderItem.UnitCostBolivia;
                    productLotItem.CalculateUnitPrice();

                    await _productService.AddAsync(productLotItem);

                    purchaseOrderItem.ProductId = productLotItem.Id;
                    purchaseOrderItem.Summary = productLotItem.Number;
                }

                var inventoryTransaction = new InventoryTransaction()
                {
                    WarehouseId = 2,
                    ProductId = purchaseOrderItem.ProductId,
                    ModuleId = entity!.Id,
                    ModuleName = moduleName,
                    ModuleCode = "GR",
                    ModuleNumber = entity.Number ?? string.Empty,
                    MovementDate = entity.ReceiveDate!.Value,
                    Status = (InventoryTransactionStatus)entity.Status!,
                    RequestedMovement = purchaseOrderItem.Quantity,
                    Movement = purchaseOrderItem.Quantity,
                    Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
                };

                await _inventoryTransactionService.AddAsync(inventoryTransaction);
            }
        }

        public override async Task UpdateAsync(GoodsReceive? entity)
        {
            await base.UpdateAsync(entity);
            await RecalculateChildAsync(entity?.Id);
        }


        private async Task RecalculateChildAsync(int? masterId)
        {
            var master = await _context.Set<GoodsReceive>()
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .Where(x => x.Id == masterId && x.IsNotDeleted)
                .FirstOrDefaultAsync();

            var children = await _inventoryTransactionService.GetAll()
                .Where(x => x.ModuleId == masterId && x.ModuleName == nameof(GoodsReceive))
                .ToListAsync();

            if (master != null)
            {
                foreach (InventoryTransaction inventoryTransaction in children)
                {
                    inventoryTransaction.Status = (InventoryTransactionStatus)master.Status!;
                    await _inventoryTransactionService.UpdateAsync(inventoryTransaction);
                }
            }
        }
    }
}
