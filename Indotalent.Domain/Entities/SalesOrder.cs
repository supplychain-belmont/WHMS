using Indotalent.Domain.Contracts;
using Indotalent.Domain.Enums;

namespace Indotalent.Domain.Entities
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
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
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
