using System;
using Indotalent.Models.Contracts;
namespace Indotalent.Models.Entities
{
    public class Payment : _Base
    {
        public Payment(string paymentName, Guid id)
        {
            PaymentName = paymentName;
            Id = id;
        }

        public string PaymentName { get; set; }
        public Guid Id { get; set; }
    }
}
