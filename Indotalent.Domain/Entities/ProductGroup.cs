using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class ProductGroup : _Base
    {
        public ProductGroup() { }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
