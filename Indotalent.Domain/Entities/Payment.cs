using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class Payment : _Base
    {
        public Payment(string paymentName, Guid id)
        {
            PaymentName = paymentName;
            base.RowGuid = id;
        }

        public string PaymentName { get; set; }
    }
}
