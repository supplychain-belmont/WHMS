namespace Indotalent.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }
        public double? UnitPrice { get; set; }
        public bool? Physical { get; set; }
        public int ProductGroupId { get; set; }
        public string? ProductGroup { get; set; }
        public int UnitMeasureId { get; set; }
        public string? UnitMeasure { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
