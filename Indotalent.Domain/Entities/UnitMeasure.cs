using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class UnitMeasure : _Base
    {
        public UnitMeasure() { }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
