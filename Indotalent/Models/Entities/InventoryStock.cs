using Indotalent.DTOs;
using Indotalent.Models.Contracts;

using static System.Math;

namespace Indotalent.Models.Entities;

public class InventoryStock : _Base
{
    public int? WarehouseId { get; set; }
    public string? Warehouse { get; set; }
    public int? ProductId { get; set; }
    public string? Product { get; set; }
    public decimal Stock { get; set; }
    public decimal Reserved { get; set; }
    public decimal Incoming { get; set; }


    public static InvenStockDto Parse(InventoryStock data)
    {
        return new InvenStockDto
        {
            Id = data.Id,
            RowGuid = data.RowGuid,
            CreatedAtUtc = data.CreatedAtUtc,
            WarehouseId = data.WarehouseId,
            Warehouse = data.Warehouse,
            ProductId = data.ProductId,
            Product = data.Product,
            Stock = data.Stock,
            Reserved = data.Reserved,
            ReservedPercentage = Round(Min(data.Reserved / data.Stock * 100, 100), 2),
            Incoming = data.Incoming
        };
    }
}
