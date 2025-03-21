using System.ComponentModel.DataAnnotations;

using Indotalent.Domain.Enums;

namespace Indotalent.DTOs
{
    public class SalesReturnDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public DateTime? ReturnDate { get; set; }
        public SalesReturnStatus? Status { get; set; }
        public string? DeliveryOrder { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Customer { get; set; }
        [Key] public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
