using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;

namespace Indotalent.Application.Products;

public class AssemblyProcessor
{
    public SalesOrder CreateSalesOrder(Assembly? assembly, string salesOrderNumber)
    {
        var salesOrder = new SalesOrder
        {
            Number = salesOrderNumber,
            TaxAmount = 0.0m,
            AfterTaxAmount = 0.0m,
            BeforeTaxAmount = 0.0m,
            CustomerId = 0,
            TaxId = 0,
            OrderDate = DateTime.UtcNow,
            Description = "Assembly Order",
            OrderStatus = SalesOrderStatus.Draft
        };

        return salesOrder;
    }

    public List<SalesOrderItem> CreateSalesOrderItem(int SalesOrderId, List<AssemblyChild> children)
    {
        var orderItems = new List<SalesOrderItem>();

        foreach (var child in children)
        {
            var orderItem = new SalesOrderItem
            {
                SalesOrderId = SalesOrderId,
                ProductId = child.ProductId,
                Quantity = child.Quantity,
            };
            orderItems.Add(orderItem);
        }
        return orderItems;
    }
}
