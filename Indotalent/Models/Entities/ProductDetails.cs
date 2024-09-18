using Indotalent.Models.Contracts;


namespace Indotalent.Models.Entities
{
    public class ProductDetails : _Base
    {
        public required int ProductId { get; set; }
        public Product? Product { get; set; }

        public required int NationalProductOrderId { get; set; }
        public NationalProductOrder? NationalProductOrder { get; set; }

        public string? Dimensions { get; set; }
        public string? Brand { get; set; }
        public string? Service { get; set; }
    }
}
