using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class CustomerCategory : _Base
    {
        public CustomerCategory() { }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
