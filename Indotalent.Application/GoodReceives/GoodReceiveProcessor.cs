using Indotalent.Application.Products;
using Indotalent.Domain.Entities;
using Indotalent.Domain.Enums;

namespace Indotalent.Application.GoodReceives;

public class GoodReceiveProcessor
{
    public Product CreateProduct(ProductProcessor productProcessor,
        PurchaseOrderItem purchaseOrderItem)
    {
        var product = purchaseOrderItem.Product;
        var lot = purchaseOrderItem.LotItem!.Lot;
        var productLotItem = productProcessor.Clone(product!);
        productLotItem.RowGuid = Guid.NewGuid();
        productLotItem.Id = 0;
        productLotItem.Name = $"{product!.Name}/{lot!.Name}";
        productLotItem.UnitCost = purchaseOrderItem.UnitCostBolivia;
        productProcessor.CalculateUnitPrice(productLotItem);

        return productLotItem;
    }

    public InventoryTransaction CreateInventoryTransaction(PurchaseOrderItem purchaseOrderItem, GoodsReceive entity,
        string numberSequence)
    {
        return new InventoryTransaction()
        {
            WarehouseId = 2,
            ProductId = purchaseOrderItem.ProductId,
            ModuleId = entity!.Id,
            ModuleName = nameof(GoodsReceive),
            ModuleCode = "GR",
            ModuleNumber = entity.Number ?? string.Empty,
            MovementDate = entity.ReceiveDate!.Value,
            Status = (InventoryTransactionStatus)entity.Status!,
            RequestedMovement = purchaseOrderItem.Quantity,
            Movement = purchaseOrderItem.Quantity,
            Number = numberSequence,
        };
    }
}
