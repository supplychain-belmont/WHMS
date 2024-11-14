using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities;

public class LotItem : _Base
{
    public int LotId { get; set; }
    public Lot? Lot { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; } = 0;
    public required decimal UnitCostBrazil { get; set; }
    public decimal UnitCostDiscounted { get; set; } = 0;
}
