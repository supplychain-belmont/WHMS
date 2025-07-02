using System.ComponentModel.DataAnnotations;

using Indotalent.Domain.Enums;

namespace Indotalent.DTOs
{
    public class SalesOrderDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public DateTime? OrderDate { get; set; }
        public SalesOrderStatus? Status { get; set; }
        public string? Description { get; set; }
        public string? Customer { get; set; }
        public int? CustomerId { get; set; }
        public string? Tax { get; set; }
        public int? TaxId { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public decimal? BeforeTaxAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AfterTaxAmount { get; set; }
    }
}
