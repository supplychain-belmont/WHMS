﻿using Indotalent.Domain.Contracts;
using Indotalent.Domain.Enums;

namespace Indotalent.Domain.Entities
{
    public class StockCount : _Base
    {
        public StockCount() { }
        public string? Number { get; set; }
        public DateTime? CountDate { get; set; }
        public StockCountStatus? Status { get; set; }
        public string? Description { get; set; }
        public required int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
    }
}
