﻿using System;

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
        public decimal UnitPrice { get; set; } = 0;
        public decimal Quantity { get; set; } = 1;
        public decimal? Total { get; set; } = 0;

        public void RecalculateTotal()
        {
            Total = Quantity * UnitPrice;
        }
    }
}
