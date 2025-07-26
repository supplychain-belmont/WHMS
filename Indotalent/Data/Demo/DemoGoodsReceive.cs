using Indotalent.Applications.GoodsReceives;
using Indotalent.Applications.InventoryTransactions;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.Applications.Warehouses;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;
using Indotalent.Persistence;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Data.Demo
{
    public static class DemoGoodsReceive
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var goodsReceiveService = services.GetRequiredService<GoodsReceiveService>();
            var purchaseOrderService = services.GetRequiredService<PurchaseOrderService>();
            var purchaseOrderItemService = services.GetRequiredService<PurchaseOrderItemService>();
            var warehouseService = services.GetRequiredService<WarehouseService>();
            var numberSequenceService = services.GetRequiredService<NumberSequenceService>();
            var inventoryTransactionService = services.GetRequiredService<InventoryTransactionService>();
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            Random random = new Random();
            int goodsReceiveStatusLength = Enum.GetNames(typeof(GoodsReceiveStatus)).Length;

            var purchaseOrders = await purchaseOrderService
                .GetAll()
                .Where(x => x.OrderStatus == PurchaseOrderStatus.Confirmed)
                .ToListAsync();

            var warehouses = await warehouseService
                .GetAll()
                .Where(x => x.SystemWarehouse == false)
                .Select(x => x.Id)
                .ToArrayAsync();

            foreach (var purchaseOrder in purchaseOrders)
            {
                var goodsReceive = new GoodsReceive
                {
                    Number = numberSequenceService.GenerateNumber(nameof(GoodsReceive), "", "GR"),
                    ReceiveDate = purchaseOrder.OrderDate?.AddDays(random.Next(1, 5)),
                    Status = (GoodsReceiveStatus)random.Next(0, goodsReceiveStatusLength),
                    PurchaseOrderId = purchaseOrder.Id,
                };
                await goodsReceiveService.AddAsync(goodsReceive);

                var transactions = await dbContext.Set<InventoryTransaction>()
                    .Where(x => x.ModuleId == goodsReceive.Id)
                    .ToListAsync();

                if (transactions.Count != 0)
                {
                    foreach (InventoryTransaction inventoryTransaction in transactions)
                    {
                        inventoryTransaction.WarehouseId = DbInitializer.GetRandomValue(warehouses, random);
                        await inventoryTransactionService.UpdateAsync(inventoryTransaction);
                    }

                    continue; // Skip if transactions already exist for this GoodsReceive
                }

                var items = await purchaseOrderItemService
                    .GetAll()
                    .Include(x => x.Product)
                    .Where(x => x.PurchaseOrderId == purchaseOrder.Id && x.Product!.Physical == true)
                    .ToListAsync();

                foreach (var item in items)
                {
                    var inventoryTransaction = new InventoryTransaction
                    {
                        ModuleId = goodsReceive.Id,
                        ModuleName = nameof(GoodsReceive),
                        ModuleCode = "GR",
                        ModuleNumber = goodsReceive.Number,
                        MovementDate = goodsReceive.ReceiveDate!.Value,
                        Status = (InventoryTransactionStatus)goodsReceive.Status,
                        Number = numberSequenceService.GenerateNumber(nameof(InventoryTransaction), "", "IVT"),
                        WarehouseId = DbInitializer.GetRandomValue(warehouses, random),
                        ProductId = item.ProductId,
                        Movement = item.Quantity
                    };

                    inventoryTransactionService.CalculateInvenTrans(inventoryTransaction);
                    await inventoryTransactionService.AddAsync(inventoryTransaction);
                }
            }
        }
    }
}
