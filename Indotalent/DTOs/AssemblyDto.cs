namespace Indotalent.DTOs;

public class AssemblyDto
{
    public int? Id { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public string? Description { get; set; }
}
