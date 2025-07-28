using System;

using Indotalent.Domain.Contracts;
namespace Indotalent.Domain.Entities
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
