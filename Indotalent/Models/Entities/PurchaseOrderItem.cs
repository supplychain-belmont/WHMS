﻿using Indotalent.Models.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Models.Entities
{
    public class PurchaseOrderItem : _Base
    {
        public PurchaseOrderItem() { }
        public required int PurchaseOrderId { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public PurchaseOrder? PurchaseOrder { get; set; }

        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Summary { get; set; }
        public decimal UnitCost { get; set; } = 0;
        public required decimal UnitCostBrazil { get; set; }
        public required decimal UnitCostBolivia { get; set; }
        public decimal UnitCostBoliviaBs { get; set; } = 0;
        public decimal UnitCostDiscounted { get; set; } = 0;

        public decimal TransportCost { get; set; } = 0;
        public decimal AgencyCost { get; set; } = 0;
        public decimal TotalShippingCost { get; set; } = 0;

        public decimal UnitPrice { get; set; } = 0;
        public decimal Quantity { get; set; } = 1;
        public decimal WeightedPercentageM3 { get; set; } = 0;
        public decimal WeightedByCost { get; set; } = 0;
        public decimal? Total { get; set; } = 0;

        public int? AssemblyId { get; set; }
        public bool IsAssembly { get; set; }
        public bool ShowOrderItem { get; set; } = true;

        public int? LotItemId { get; set; }
        public LotItem? LotItem { get; set; }

        public void RecalculateTotal()
        {
            TotalShippingCost = TransportCost + AgencyCost;
            UnitCostBolivia = UnitCostDiscounted + TotalShippingCost;
            Total = Quantity * (UnitCostDiscounted + TotalShippingCost);
            UnitCostBoliviaBs = Total.Value * 6.92m;
        }

        public void RecalculateWeightedM3(decimal m3, decimal containerM3)
        {
            WeightedPercentageM3 = (m3 / containerM3) * 100;
        }

        public void RecalculateTransportCost(decimal totalTransportCost, decimal TotalAgencyCost)
        {
            TransportCost = (WeightedPercentageM3 * totalTransportCost) / 100;
        }
    }
}
