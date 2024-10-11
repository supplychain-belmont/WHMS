using Indotalent.Models.Enums;

namespace Indotalent.DTOs
{
    public class PurchaseOrderDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal ContainerM3 { get; set; }
        public decimal TotalTransportContainerCost { get; set; }
        public decimal TotalAgencyCost { get; set; }
        public PurchaseOrderStatus? Status { get; set; }
        public string? Description { get; set; }
        public int VendorId { get; set; }
        public string? Vendor { get; set; }
        public int? TaxId { get; set; }
        public string? Tax { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
    }
}
