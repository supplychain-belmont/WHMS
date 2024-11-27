using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities;

public class InventoryStock : _Base
{
    public int? WarehouseId { get; set; }
    public string? Warehouse { get; set; }
    public int? ProductId { get; set; }
    public string? Product { get; set; }
    public decimal Stock { get; set; }
    public decimal Reserved { get; set; }
    public decimal ReservedPercentage { get; set; }
    public decimal Incoming { get; set; }
}
