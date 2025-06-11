using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Applications.SalesOrderItems;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.DeliveryOrders
{
    public class DeliveryOrderService : Repository<DeliveryOrder>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly SalesOrderItemService _salesOrderItemService;
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly InventoryStockService _inventoryStockService;

        public DeliveryOrderService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            SalesOrderItemService salesOrderItemService,
            InventoryTransactionService inventoryTransactionService,
            InventoryStockService inventoryStockService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _salesOrderItemService = salesOrderItemService;
            _inventoryTransactionService = inventoryTransactionService;
            _inventoryStockService = inventoryStockService;
        }

        public override async Task AddAsync(DeliveryOrder? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(DeliveryOrder), "", "DO");
            await base.AddAsync(entity);

            var salesItems = await _salesOrderItemService.GetAll()
                .Include(item => item.Product)
                .Where(item => item.SalesOrderId == entity!.SalesOrderId)
                .ToListAsync();

            foreach (SalesOrderItem salesOrderItem in salesItems)
            {
                var transaction = await CreateTransaction(salesOrderItem.ProductId, salesOrderItem.Quantity, entity);
                await _inventoryTransactionService.AddAsync(transaction);
            }
        }

        private Task<InventoryTransaction> CreateTransaction(int productId, decimal quantity,
            DeliveryOrder entity)
        {
            return Task.FromResult(new InventoryTransaction
            {
                WarehouseId = 1,
                ProductId = productId,
                ModuleId = entity.Id,
                ModuleName = nameof(DeliveryOrder),
                ModuleCode = "DO",
                ModuleNumber = entity.Number ?? string.Empty,
                MovementDate = entity.DeliveryDate!.Value,
                Status = (InventoryTransactionStatus)entity.Status!,
                RequestedMovement = quantity,
                Movement = quantity,
                Number = _numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT")
            });
        }

        public override async Task UpdateAsync(DeliveryOrder? entity)
        {
            var children = await _inventoryTransactionService
                .GetAll()
                .Where(x => x.ModuleId == entity!.Id && x.ModuleName == nameof(DeliveryOrder))
                .ToListAsync();

            if (entity!.Status == DeliveryOrderStatus.Confirmed)
            {
                var stockInfo = await _inventoryStockService
                    .GetAll()
                    .Where(it =>
                        children.Select(x => x.ProductId).Contains(it.ProductId!.Value) &&
                        children.Select(x => x.WarehouseId).Contains(it.WarehouseId!.Value))
                    .Select(it =>
                        new { it.ProductId, it.Product, it.WarehouseId, it.Stock })
                    .ToListAsync();

                var products = new List<string>();
                foreach (InventoryTransaction item in children)
                {
                    var inventoryStock = stockInfo
                        .Find(it =>
                            it.ProductId == item.ProductId && it.WarehouseId == item.WarehouseId
                        );

                    if (inventoryStock is null) continue;
                    if (item.Movement <= inventoryStock.Stock)
                    {
                        continue;
                    }

                    entity.Status = DeliveryOrderStatus.Draft;
                    products.Add(inventoryStock.Product!);
                }

                if (entity.Status == DeliveryOrderStatus.Draft)
                {
                    throw new ArgumentException($"Stock is not enough for product(s): {string.Join(", ", products)}");
                }
            }

            foreach (var item in children)
            {
                item.Status = (InventoryTransactionStatus)entity.Status!;
                await _inventoryTransactionService.UpdateAsync(item);
            }

            await base.UpdateAsync(entity);
        }
    }
}
