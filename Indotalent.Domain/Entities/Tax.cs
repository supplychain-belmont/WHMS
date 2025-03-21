using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class Tax : _Base
    {
        public Tax() { }
        public string? Name { get; set; }
        public decimal? Percentage { get; set; }
        public string? Description { get; set; }
    }
}
