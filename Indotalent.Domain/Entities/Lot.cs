using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities;

public class Lot : _Base
{
    public string? Number { get; set; }
    public string? Name { get; set; }
    public decimal ContainerM3 { get; set; }
    public decimal TotalTransportContainerCost { get; set; }
    public decimal TotalAgencyCost { get; set; }
    public DateTime LotDate { get; set; }
}
