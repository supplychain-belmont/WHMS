namespace Indotalent.DTOs
{
    public class NationalProductOrderDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? OrderStatus { get; set; }
        public string? Description { get; set; }
        public int? VendorId { get; set; }
        public int? TaxId { get; set; }
        public double? AmountPayable { get; set; }
        public double? TaxAmount { get; set; }
        public double? AfterTaxAmount { get; set; }
        public Guid? RowGuid { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public bool? IsNotDeleted { get; set; }
        public string? PaymentId { get; set; }
        public double? AmountPaid { get; set; }
        public double? Balance { get; set; }
        public bool? Invoice { get; set; }
        public double? FiscalCredit { get; set; }
        public int? PaymentID { get; set; }
    }
}
