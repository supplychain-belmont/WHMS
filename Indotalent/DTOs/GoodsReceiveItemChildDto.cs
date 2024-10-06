namespace Indotalent.DTOs
{
    public class GoodsReceiveItemChildDto
    {
        public int? Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int? WarehouseId { get; set; }
        public int? ProductId { get; set; }
        public decimal RequestedMovement { get; set; }
        public decimal? Movement { get; set; }
    }
}
