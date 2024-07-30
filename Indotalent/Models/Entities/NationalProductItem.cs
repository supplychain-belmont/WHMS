using System;

using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
{
    public class NationalProductOrderItem : _Base
    {
        public int NationalProductOrderId { get; set; }
        public int ProductId { get; set; }
        public string? Summary { get; set; }
        public float UnitPrice { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        public float ManPowerCost { get; set; }
        public float MaterialCost { get; set; }
        public float ShippingCost { get; set; }
        public float TotalAmount { get; set; }
        public float DiscountCost { get; set; }
        public float AmountPayable { get; set; }
        public float Utility1 { get; set; }
        public float Utility2 { get; set; }
        public float UnitPriceInvoice { get; set; }
        public float UnitPriceNoInvoice { get; set; }
        public string? ColorCode { get; set; }
    }
}
