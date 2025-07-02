using Indotalent.Application.GoodReceives;
using Indotalent.Application.Products;
using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.GoodsReceives
{
    public class GoodsReceiveService : Repository<GoodsReceive>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly ProductService _productService;
        private readonly ProductProcessor _productProcessor;
        private readonly GoodReceiveProcessor _goodReceiveProcessor;

        public GoodsReceiveService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            PurchaseOrderItemService purchaseOrderItemService,
            InventoryTransactionService inventoryTransactionService,
            ProductService productService,
            ProductProcessor productProcessor,
            GoodReceiveProcessor goodReceiveProcessor) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _purchaseOrderItemService = purchaseOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
            _productService = productService;
            _productProcessor = productProcessor;
            _goodReceiveProcessor = goodReceiveProcessor;
        }

        public override async Task AddAsync(GoodsReceive? entity)
        {
            await base.AddAsync(entity);
            var purchaseItems = await _purchaseOrderItemService.GetAll()
                .Include(item => item.Product)
                .Include(item => item.LotItem)
                .ThenInclude(lotItem => lotItem!.Lot)
                .Where(item => item.PurchaseOrderId == entity!.PurchaseOrderId)
                .ToListAsync();

            var transactions = new List<InventoryTransaction>();
            foreach (PurchaseOrderItem purchaseOrderItem in purchaseItems)
            {
                if (purchaseOrderItem.LotItemId.HasValue)
                {
                    var productLotItem = _goodReceiveProcessor.CreateProduct(_productProcessor, purchaseOrderItem);

                    var exist = await _productService.GetAll()
                        .Where(p => p.Name == productLotItem.Name)
                        .FirstOrDefaultAsync();

                    if (exist == null)
                    {
                        await _productService.AddAsync(productLotItem);
                    }
                    else
                    {
                        productLotItem = exist;
                    }

                    purchaseOrderItem.ProductId = productLotItem.Id;
                    purchaseOrderItem.Summary = productLotItem.Number;
                }

                var numberSequence = _numberSequenceService
                    .GenerateNumber(nameof(InventoryTransaction), "", "IVT");

                transactions.Add(_goodReceiveProcessor
                    .CreateInventoryTransaction(purchaseOrderItem, entity!, numberSequence));
            }

            await _inventoryTransactionService.AddRangeAsync(transactions);
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
