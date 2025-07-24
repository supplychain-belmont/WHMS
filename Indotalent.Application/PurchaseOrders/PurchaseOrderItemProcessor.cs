using Indotalent.Domain.Entities;

namespace Indotalent.Application.PurchaseOrders;

public class PurchaseOrderItemProcessor
{
    public void RecalculateWeightedM3(PurchaseOrderItem purchaseOrderItem, decimal m3, decimal containerM3)
    {
        purchaseOrderItem.WeightedPercentageM3 = (m3 / containerM3) * 100;
    }

    public void RecalculateTransportCost(PurchaseOrderItem purchaseOrderItem, decimal totalTransportCost,
        decimal TotalAgencyCost)
    {
        purchaseOrderItem.TransportCost = (purchaseOrderItem.WeightedPercentageM3 * totalTransportCost) / 100;
    }

    public void RecalculateTotal(PurchaseOrderItem purchaseOrderItem)
    {
        purchaseOrderItem.TotalShippingCost = purchaseOrderItem.TransportCost + purchaseOrderItem.AgencyCost;
        purchaseOrderItem.UnitCostBolivia = purchaseOrderItem.UnitCostDiscounted + purchaseOrderItem.TotalShippingCost;
        purchaseOrderItem.Total = purchaseOrderItem.Quantity *
                                  (purchaseOrderItem.UnitCostDiscounted + purchaseOrderItem.TotalShippingCost);
        purchaseOrderItem.UnitCostBoliviaBs = purchaseOrderItem.Total.Value * 6.92m;
    }

    public void CalculateCostsWithoutShipping(PurchaseOrderItem purchaseOrderItem)
    {
        purchaseOrderItem.TransportCost = 0;
        purchaseOrderItem.TotalShippingCost = 0;
        purchaseOrderItem.UnitCostBolivia = purchaseOrderItem.UnitCostDiscounted;
        purchaseOrderItem.Total = purchaseOrderItem.Quantity * purchaseOrderItem.UnitCostDiscounted;
        purchaseOrderItem.UnitCostBoliviaBs = purchaseOrderItem.Total.Value * 6.92m;
    }

    public void CalculateCosts(PurchaseOrderItem purchaseOrderItem, decimal m3, decimal containerM3,
        decimal totalTransportCost, decimal totalAgencyCost)
    {
        RecalculateWeightedM3(purchaseOrderItem, m3, containerM3);
        RecalculateTransportCost(purchaseOrderItem, totalTransportCost, totalAgencyCost);
        RecalculateTotal(purchaseOrderItem);
    }
}
