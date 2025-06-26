using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;

namespace Indotalent.Application.Products;

public class AssemblyProcessor
{
    const string counter = "900";
    static int index;

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

    public List<SalesOrderItem> CreateSalesOrderItem(int salesOrderId, List<AssemblyChild> children, int quantity = 1)
    {
        var orderItems = new List<SalesOrderItem>();

        foreach (var child in children)
        {
            var orderItem = new SalesOrderItem
            {
                SalesOrderId = salesOrderId,
                ProductId = child.ProductId,
                Quantity = child.Quantity * quantity,
            };
            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    public SalesOrderItem CreateSalesOrderItem(int salesOrderId, Assembly? assembly, int quantity = 1)
    {
        return new SalesOrderItem
        {
            SalesOrderId = salesOrderId,
            ProductId = assembly!.ProductId,
            Quantity = 1 * quantity,
        };
    }

    public List<InventoryTransaction> CreateInventoryTransactions(Assembly assembly, List<AssemblyChild> children,
        int warehouseId,
        int quantity = 1)
    {
        var transactions = new List<InventoryTransaction>();

        foreach (var transaction in children.Select(child => new InventoryTransaction
        {
            WarehouseId = warehouseId,
            WarehouseFromId = warehouseId,
            WarehouseToId = warehouseId,
            ProductId = child.ProductId,
            ModuleId = assembly.Id,
            ModuleName = nameof(Assembly),
            ModuleCode = "AO",
            ModuleNumber = child.Product?.Number!,
            MovementDate = DateTime.UtcNow,
            Movement = child.Quantity * quantity,
            RequestedMovement = child.Quantity * quantity,
            Number = $"{counter}{index}{DateTime.Now:yyyyMMdd}AO",
            Status = InventoryTransactionStatus.Confirmed,
            TransType = InventoryTransType.Out
        }))
        {
            transactions.Add(transaction);
            index++;
        }

        return transactions;
    }

    public InventoryTransaction CreateAssemblyTransaction(Assembly assembly, int warehouseId, int quantity = 1)
    {
        ++index;
        return new InventoryTransaction
        {
            WarehouseId = warehouseId,
            WarehouseFromId = warehouseId,
            WarehouseToId = warehouseId,
            ProductId = assembly.ProductId,
            ModuleId = assembly.Id,
            ModuleName = nameof(Assembly),
            ModuleCode = "AO",
            ModuleNumber = assembly.Product?.Number!,
            MovementDate = DateTime.UtcNow,
            Movement = quantity,
            RequestedMovement = quantity,
            Number = $"{counter}{index}{DateTime.Now:yyyyMMdd}AO",
            Status = InventoryTransactionStatus.Confirmed,
            TransType = InventoryTransType.In
        };
    }
}
