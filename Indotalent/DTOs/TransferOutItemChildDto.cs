namespace Indotalent.DTOs
{
    public class TransferOutItemChildDto
    {
        public int? Id { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public int? ProductId { get; set; }
        public decimal Stock { get; set; }
        public decimal Movement { get; set; }
    }
}
