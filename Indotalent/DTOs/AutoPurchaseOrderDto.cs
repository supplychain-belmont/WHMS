namespace Indotalent.DTOs;

public class AutoPurchaseOrderDto
{
    public int VendorId { get; set; }
    public int TaxId { get; set; }
    public decimal ContainerM3 { get; set; }
    public int? LotId { get; set; }
    public string? Description { get; set; }
}
