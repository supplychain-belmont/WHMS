using System;
using System.ComponentModel.DataAnnotations;

using Indotalent.Models.Contracts;
namespace Indotalent.Models.Entities
{
    public class NationalProductOrderItem : _Base
    {
        public int NationalProductOrderId { get; set; }
        public int ProductId { get; set; }
        public string? Summary { get; set; }
        public double UnitPrice { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
        public double ManPowerCost { get; set; }
        public double MaterialCost { get; set; }
        public double ShippingCost { get; set; }
        public double TotalAmount { get; set; }
        public double DiscountCost { get; set; }
        public double AmountPayable { get; set; }
        public double Utility1 { get; set; }
        public double Utility2 { get; set; }
        public double UnitPriceInvoice { get; set; }
        public double UnitPriceNoInvoice { get; set; }
        public string? ColorCode { get; set; }
        public int Id { get; set; }
    }
}
