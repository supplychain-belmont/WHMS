using Indotalent.Application.PurchaseOrders;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.Applications.Taxes;
using Indotalent.Applications.Vendors;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;

namespace Indotalent.Data.Demo
{
    public static class DemoPurchaseOrder
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var purchaseOrderService = services.GetRequiredService<PurchaseOrderService>();
            var purchaseOrderItemService = services.GetRequiredService<PurchaseOrderItemService>();
            var vendorService = services.GetRequiredService<VendorService>();
            var taxSerice = services.GetRequiredService<TaxService>();
            var productService = services.GetRequiredService<ProductService>();
            var numberSequenceService = services.GetRequiredService<NumberSequenceService>();
            var purchaseOrderItemProcessor = services.GetRequiredService<PurchaseOrderItemProcessor>();

            Random random = new Random();
            int orderStatusLength = Enum.GetNames(typeof(PurchaseOrderStatus)).Length;
            var vendors = vendorService.GetAll().Select(x => x.Id).ToArray();
            var taxes = taxSerice.GetAll().Select(x => x.Id).ToArray();
            var products = productService.GetAll().ToList();

            var dateFinish = DateTime.Now;
            var dateStart = new DateTime(dateFinish.AddMonths(-12).Year, dateFinish.AddMonths(-12).Month, 1);

            for (DateTime date = dateStart; date < dateFinish; date = date.AddMonths(1))
            {
                DateTime[] transactionDates = DbInitializer.GetRandomDays(date.Year, date.Month, 6);

                foreach (DateTime transDate in transactionDates)
                {
                    var purchaseOrder = new PurchaseOrder
                    {
                        Number = numberSequenceService.GenerateNumber(nameof(PurchaseOrder), "", "PO"),
                        OrderDate = transDate,
                        OrderStatus = (PurchaseOrderStatus)random.Next(0, orderStatusLength),
                        ContainerM3 = 83.33m,
                        TotalTransportContainerCost = random.Next(500, 1000),
                        TotalAgencyCost = random.Next(500, 1000),
                        VendorId = DbInitializer.GetRandomValue(vendors,
                            random),
                        TaxId = DbInitializer.GetRandomValue(taxes, random),
                    };
                    await purchaseOrderService.AddAsync(purchaseOrder);

                    int numberOfProducts = random.Next(3, 6);
                    var cost = random.Next(100, 500);
                    for (int i = 0; i < numberOfProducts; i++)
                    {
                        var product = products[random.Next(0, products.Count)];
                        var purchaseOrderItem = new PurchaseOrderItem
                        {
                            PurchaseOrderId = purchaseOrder.Id,
                            ProductId = product.Id,
                            Summary = product.Number,
                            UnitCost = random.Next(100, 500),
                            UnitCostBolivia = cost,
                            UnitCostBrazil = cost,
                            TransportCost = random.Next(100, 500),
                            AgencyCost = random.Next(100, 500),
                            UnitPrice = product.UnitPrice,
                            UnitCostDiscounted = cost,
                            Quantity = random.Next(20, 50),
                        };
                        purchaseOrderItemProcessor.CalculateCosts(purchaseOrderItem, product.M3,
                            purchaseOrder.ContainerM3,
                            purchaseOrder.TotalTransportContainerCost, purchaseOrder.TotalAgencyCost);
                        await purchaseOrderItemService.AddAsync(purchaseOrderItem);
                    }
                }
            }
        }
    }
}
