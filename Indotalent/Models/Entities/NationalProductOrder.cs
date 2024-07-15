using Indotalent.Models.Contracts;
using System;

namespace Indotalent.Models.Entities
{
    public class NationalProductOrder : _Base
    {
        public required string Number { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public string? Description { get; set; }
        public int VendorId { get; set; }
        public int TaxId { get; set; }
        public double AmountPayable { get; set; }
        public double TaxAmount { get; set; }
        public double AfterTaxAmount { get; set; }
        public Guid RowGuid { get; set; }
        public string CreatedByUserId { get; set; } = default!;
        public DateTime CreatedAtUtc { get; set; }
        public string UpdatedByUserId { get; set; } = default!;
        public DateTime UpdatedAtUtc { get; set; }
        public bool IsNotDeleted { get; set; }
        public string PaymentId { get; set; } = default!;
        public double AmountPaid { get; set; }
        public double Balance { get; set; }
        public bool Invoice { get; set; }
        public double FiscalCredit { get; set; }
        public int PaymentID { get; set; }
    }
}
