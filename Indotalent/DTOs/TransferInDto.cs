﻿using Indotalent.Domain.Enums;

namespace Indotalent.DTOs
{
    public class TransferInDto
    {
        public int? Id { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public TransferStatus? Status { get; set; }
        public int TransferOutId { get; set; }
        public string? TransferOut { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int WarehouseFromId { get; set; }
        public string? WarehouseFrom { get; set; }
        public int WarehouseToId { get; set; }
        public string? WarehouseTo { get; set; }
        public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
