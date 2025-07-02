using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities;

public class Assembly : _Base
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string? Description { get; set; }
}
