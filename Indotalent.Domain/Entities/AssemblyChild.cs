using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities;

public class AssemblyChild : _Base
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int AssemblyId { get; set; }
    public Assembly? Assembly { get; set; }
    public int Quantity { get; set; }
}
