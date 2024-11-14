namespace Indotalent.DTOs;

public class LotDto
{
    public int Id { get; set; }
    public string? Number { get; set; }
    public string? Name { get; set; }
    public decimal ContainerM3 { get; set; }
    public decimal TotalTransportContainerCost { get; set; }
    public decimal TotalAgencyCost { get; set; }
    public DateTime LotDate { get; set; }
    public Guid RowGuid { get; set; }
    public DateTime? CreatedAtUtc { get; set; }
}
