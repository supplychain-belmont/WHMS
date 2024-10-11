using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
{
    public class Product : _Base
    {
        public Product() { }
        public required string Name { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public required decimal UnitPrice { get; set; }
        public decimal? UnitPrice1 { get; set; }
        public decimal? UnitPrice2 { get; set; }
        public decimal? UnitPrice3 { get; set; }

        public decimal M3 { get; set; }

        public bool Physical { get; set; } = true;
        public required int UnitMeasureId { get; set; }
        public UnitMeasure? UnitMeasure { get; set; }
        public required int ProductGroupId { get; set; }
        public ProductGroup? ProductGroup { get; set; }
        public string? Color { get; set; }
        public string? ColorCode { get; set; }
        public string? Material { get; set; }
        public string? TapestryCode { get; set; }
        public bool IsNationalProduct { get; set; }
        public string? ProductCategory { get; set; }
    }
}
