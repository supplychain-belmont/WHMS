using Indotalent.Domain.Entities;

namespace Indotalent.Application.PurchaseOrders;

public class PurchaseOrderProcessor
{
    public void RecalculateParent(PurchaseOrder master, List<PurchaseOrderItem> children)
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
    }

    public void CalculatePurchaseOrder(Lot lot, PurchaseOrder entity)
    {
        entity.ContainerM3 = lot!.ContainerM3;
        entity.TotalTransportContainerCost = lot!.TotalTransportContainerCost;
        entity.TotalAgencyCost = lot!.TotalAgencyCost;
    }

    public List<PurchaseOrderItem> CreatePurchaseOrderItems(List<LotItem> lotItems, PurchaseOrder purchaseOrder)
    {
        return lotItems.Select(lotItem => new PurchaseOrderItem
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
        })
            .ToList();
    }
}
