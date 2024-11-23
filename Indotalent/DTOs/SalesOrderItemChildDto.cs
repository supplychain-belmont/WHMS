namespace Indotalent.DTOs
{
    public class SalesOrderItemChildDto
    {
        public int? Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int? SalesOrderId { get; set; }
        public int ProductId { get; set; }
        public string? Summary { get; set; }


        #region Pricing

        public decimal? UnitCost { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? UnitPrice40 { get; set; }
        public decimal? UnitPrice50 { get; set; }
        public decimal? UnitPrice60 { get; set; } = 0;

        #endregion

        public decimal? UnitPriceDiscount { get; set; }
        public decimal? UnitPriceDiscountPercentage { get; set; }
        public decimal? Commission { get; set; }
        public decimal? GrossMargin { get; set; }
        public decimal? GrossContribution { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
