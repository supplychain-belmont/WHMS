using Indotalent.Models.Contracts;
using Indotalent.Models.Enums;

namespace Indotalent.Models.Entities
{
    public class SalesOrder : _Base
    {
        public SalesOrder() { }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public SalesOrderStatus? OrderStatus { get; set; }
        public string? Description { get; set; }
        public required int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public required int TaxId { get; set; }
        public Tax? Tax { get; set; }
        public double? BeforeTaxAmount { get; set; }
        public double? TaxAmount { get; set; }
        public double? AfterTaxAmount { get; set; }
        public int? FinalSaleAmount { get; set; }
        public string? PaymentId { get; set; }
        public int? CreditDuesId { get; set; }
        public bool? CreditExistence { get; set; }
        public int? Balance { get; set; }
        public float? It { get; set; }
        public bool Invoice { get; set; }
        public int? InvoicedAmount { get; set; }
        public int? GrossProfit { get; set; }
        public float? GrossMargin { get; set; }
        public float? DistributionMargin { get; set; }
        public float? Revenue { get; set; }
        public float? Commission { get; set; }
    }
}
