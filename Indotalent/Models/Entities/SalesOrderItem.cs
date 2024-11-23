using System;

using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
{
    public class SalesOrderItem : _Base
    {
        public SalesOrderItem() { }
        public required int SalesOrderId { get; set; }
        public SalesOrder? SalesOrder { get; set; }
        public required int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Summary { get; set; }

        #region Pricing

        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? UnitPrice40 { get; set; }
        public decimal? UnitPrice50 { get; set; }
        public decimal? UnitPrice60 { get; set; } = 0;

        #endregion

        public decimal UnitPriceDiscount { get; set; }
        public decimal UnitPriceDiscountPercentage { get; set; }
        public decimal Commission { get; set; }
        public decimal GrossMargin { get; set; }
        public decimal GrossContribution { get; set; }

        public decimal Quantity { get; set; } = 1;
        public decimal? Total { get; set; } = 0;

        public void RecalculateTotal()
        {
            UnitPriceDiscount = UnitPrice60 is null or 0
                ? 0
                : UnitPrice60.Value - UnitPrice;
            UnitPriceDiscountPercentage = UnitPrice60 is null or 0
                ? 0
                : (decimal)(UnitPriceDiscount / UnitPrice60) * 100;
            Total = Quantity * UnitPrice;
            Commission = 0.03m * Total.Value;
            GrossMargin = UnitCost == 0
                ? 0
                : ((UnitPrice - UnitCost) / UnitCost) * 100;
            GrossContribution = ((UnitPrice - UnitCost) / Total.Value) * 100;
        }
    }
}
