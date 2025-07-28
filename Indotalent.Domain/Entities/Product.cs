using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class Product : _Base
    {
        public Product() { }
        public required string Name { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }

        #region Pricing

        public required decimal UnitPrice { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitPrice40 { get; set; }
        public decimal? UnitPrice50 { get; set; }
        public decimal? UnitPrice60 { get; set; }

        #endregion

        public bool Physical { get; set; } = true;
        public bool IsAssembly { get; set; } = false;
        public int AssemblyId { get; set; }

        public required int UnitMeasureId { get; set; }
        public UnitMeasure? UnitMeasure { get; set; }
        public required int ProductGroupId { get; set; }
        public ProductGroup? ProductGroup { get; set; }

        #region Features

        public decimal M3 { get; set; }
        public string? Color { get; set; }
        public string? ColorCode { get; set; }
        public string? Material { get; set; }
        public string? TapestryCode { get; set; }
        public string? Size { get; set; }
        public bool IsNationalProduct { get; set; }
        public string? ProductCategory { get; set; }

        #endregion
    }
}
