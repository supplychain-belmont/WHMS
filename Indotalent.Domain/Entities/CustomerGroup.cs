using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class CustomerGroup : _Base
    {
        public CustomerGroup() { }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
