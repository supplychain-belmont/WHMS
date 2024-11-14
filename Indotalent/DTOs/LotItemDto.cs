namespace Indotalent.DTOs;

public class LotItemDto
{
    public int Id { get; set; }
    public int LotId { get; set; }
    public int ProductId { get; set; }
    public string? Product { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitCost { get; set; } = 0;
    public decimal UnitCostBrazil { get; set; }
    public decimal UnitCostDiscounted { get; set; } = 0;
    public Guid RowGuid { get; set; }
}
