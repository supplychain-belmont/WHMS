using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
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
        public bool IsNationalProduct { get; set; }
        public string? ProductCategory { get; set; }

        #endregion

        public void CalculateUnitPrice()
        {
            this.UnitPrice40 = this.UnitCost * 1.4m;
            this.UnitPrice50 = this.UnitCost * 1.5m;
            this.UnitPrice60 = this.UnitCost * 1.6m;
        }

        public Product Clone()
        {
            return new Product
            {
                Name = this.Name,
                Number = this.Number,
                Description = this.Description,
                UnitPrice = this.UnitPrice,
                UnitCost = this.UnitCost,
                UnitPrice40 = this.UnitPrice40,
                UnitPrice50 = this.UnitPrice50,
                UnitPrice60 = this.UnitPrice60,
                Physical = this.Physical,
                IsAssembly = this.IsAssembly,
                AssemblyId = this.AssemblyId,
                UnitMeasureId = this.UnitMeasureId,
                ProductGroupId = this.ProductGroupId,
                M3 = this.M3,
                Color = this.Color,
                ColorCode = this.ColorCode,
                Material = this.Material,
                TapestryCode = this.TapestryCode,
                IsNationalProduct = this.IsNationalProduct,
                ProductCategory = this.ProductCategory
            };
        }
    }
}
