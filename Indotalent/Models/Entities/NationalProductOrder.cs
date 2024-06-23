using System;
using System.ComponentModel.DataAnnotations;

using Indotalent.Models.Contracts;
using Indotalent.Models.Enums;
namespace Indotalent.Models.Entities
{
    public class NationalProductOrder : _Base
    {
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public NationalProductOrderStatus? OrderStatus { get; set; }
        public string? Description { get; set; }
        public int VendorId { get; set; }
        public int TaxId { get; set; }
        public double? AmountPayable { get; set; }
        public double? TaxAmount { get; set; }
        public double? AfterTaxAmount { get; set; }
        public string? PaymentId { get; set; }
        public double? AmountPaid { get; set; }
        public double? Balance { get; set; }
        public bool Invoice { get; set; }
        public double? FiscalCredit { get; set; }
        public int PaymentID { get; set; }
    }
}
