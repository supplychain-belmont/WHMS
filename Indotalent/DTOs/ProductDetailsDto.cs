namespace Indotalent.DTOs
{
    public class ProductDetailsDto
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int NationalProductOrderId { get; set; }
        public string? Dimensions { get; set; }
        public string? Brand { get; set; }
        public string? Service { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
