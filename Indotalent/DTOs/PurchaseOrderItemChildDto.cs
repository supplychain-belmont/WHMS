namespace Indotalent.DTOs
{
    public class PurchaseOrderItemChildDto
    {
        public int? Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ProductId { get; set; }
        public string? Summary { get; set; }

        public decimal? UnitCost { get; set; } = 0;
        public decimal? UnitCostBrazil { get; set; }
        public decimal? UnitCostBolivia { get; set; }
        public decimal? UnitCostBoliviaBs { get; set; } = 0;
        public decimal? UnitCostDiscounted { get; set; } = 0;

        public decimal? TransportCost { get; set; } = 0;
        public decimal? AgencyCost { get; set; } = 0;
        public decimal? TotalShippingCost { get; set; } = 0;

        public decimal? UnitPrice { get; set; }
        public decimal? M3 { get; set; }
        public decimal? WeightedPercentageM3 { get; set; } = 0;
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
