using Indotalent.Models.Enums;

namespace Indotalent.DTOs
{
    public class TransferOutDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public DateTime? TransferReleaseDate { get; set; }
        public TransferStatus? Status { get; set; }
        public int? WarehouseFromId { get; set; }
        public string? WarehouseFrom { get; set; }
        public int? WarehouseToId { get; set; }
        public string? WarehouseTo { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
