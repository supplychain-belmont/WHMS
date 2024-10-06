using System.ComponentModel.DataAnnotations;

namespace Indotalent.DTOs
{
    public class SalesOrderItemDto
    {
        public int? Id { get; set; }
        [Key] public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int SalesOrderId { get; set; }
        public string? SalesOrder { get; set; }
        public string? Customer { get; set; }
        public DateTime? OrderDate { get; set; }
        public int ProductId { get; set; }
        public string? Product { get; set; }
        public string? Summary { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
