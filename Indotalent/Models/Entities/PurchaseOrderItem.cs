using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
{
    public class PurchaseOrderItem : _Base
    {
        public PurchaseOrderItem() { }
        public required int PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Summary { get; set; }
        public decimal UnitCost { get; set; } = 0;
        public required decimal UnitCostBrazil { get; set; }
        public required decimal UnitCostBolivia { get; set; }
        public decimal UnitCostBoliviaBs { get; set; } = 0;
        public decimal UnitCostDiscounted { get; set; } = 0;

        public decimal TransportCost { get; set; } = 0;
        public decimal AgencyCost { get; set; } = 0;
        public decimal TotalShippingCost { get; set; } = 0;

        public decimal UnitPrice { get; set; } = 0;
        public decimal Quantity { get; set; } = 1;
        public decimal WeightedPercentageM3 { get; set; } = 0;
        public decimal WeightedByCost { get; set; } = 0;
        public decimal? Total { get; set; } = 0;

        public int? AssemblyId { get; set; }
        public bool IsAssembly { get; set; }
        public bool ShowOrderItem { get; set; } = true;

        public void RecalculateTotal()
        {
            TotalShippingCost = TransportCost + AgencyCost;
            Total = Quantity * (UnitCostDiscounted + TotalShippingCost);
            UnitCostBolivia = (decimal)Total;
            UnitCostBoliviaBs = UnitCostBolivia * 6.92m;
        }

        public void RecalculateWeightedM3(decimal m3, decimal containerM3)
        {
            WeightedPercentageM3 = (m3 / containerM3) * 100;
        }

        public void RecalculateTransportCost(decimal totalTransportCost, decimal TotalAgencyCost)
        {
            TransportCost = (WeightedPercentageM3 * totalTransportCost) / 100;
        }
    }
}
