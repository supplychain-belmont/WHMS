namespace Indotalent.DTOs;

public class AssemblyChildDto
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int AssemblyId { get; set; }
    public AssemblyDto? Assembly { get; set; }
    public int Quantity { get; set; }
}
