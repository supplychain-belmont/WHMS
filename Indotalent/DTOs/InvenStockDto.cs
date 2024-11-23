namespace Indotalent.DTOs
{
    public class InvenStockDto
    {
        public int Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int? WarehouseId { get; set; }
        public string? Warehouse { get; set; }
        public int? ProductId { get; set; }
        public string? Product { get; set; }
        public decimal Stock { get; set; }
        public decimal Reserved { get; set; }
        public decimal Incoming { get; set; }
    }
}
