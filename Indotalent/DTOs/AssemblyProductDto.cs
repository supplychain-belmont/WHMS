namespace Indotalent.DTOs;

public class AssemblyProductDto
{
    public int? Id { get; set; }
    public int AssemblyId { get; set; }
    public ProductDto? Assembly { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
}
