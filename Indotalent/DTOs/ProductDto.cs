namespace Indotalent.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Number { get; set; }

        #region Pricing

        public decimal UnitPrice { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitPrice40 { get; set; }
        public decimal? UnitPrice50 { get; set; }
        public decimal? UnitPrice60 { get; set; }

        #endregion

        public bool? Physical { get; set; }
        public bool IsAssembly { get; set; }
        public int AssemblyId { get; set; }

        public int ProductGroupId { get; set; }
        public string? ProductGroup { get; set; }
        public int UnitMeasureId { get; set; }
        public string? UnitMeasure { get; set; }

        public decimal? M3 { get; set; }
        public string? Color { get; set; }
        public string? ColorCode { get; set; }
        public string? Material { get; set; }
        public string? TapestryCode { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
