namespace Indotalent.DTOs
{
    public class SalesOrderItemChildDto
    {
        public int? Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int? SalesOrderId { get; set; }
        public int? ProductId { get; set; }
        public string? Summary { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }

    }
}
