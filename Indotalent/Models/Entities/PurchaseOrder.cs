using Indotalent.Models.Contracts;
using Indotalent.Models.Enums;

namespace Indotalent.Models.Entities
{
    public class PurchaseOrder : _Base
    {
        public PurchaseOrder() { }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public required decimal ContainerM3 { get; set; }
        public PurchaseOrderStatus? OrderStatus { get; set; }
        public string? Description { get; set; }
        public required int VendorId { get; set; }
        public Vendor? Vendor { get; set; }
        public required int TaxId { get; set; }
        public Tax? Tax { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
    }
}
