using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities

{
    public class ProductFileImages : _Base
    {
        public int ProductId { get; set; }
        public string? FileImagesId { get; set; }
    }
}
