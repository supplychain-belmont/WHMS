using System.ComponentModel;

namespace Indotalent.Models.Enums
{
    public enum PaymentStatus
    {
        [Description("Pending")]
        Pending = 0,
        [Description("Completed")]
        Completed = 1,
        [Description("Failed")]
        Failed = 2,
        [Description("Cancelled")]
        Cancelled = 3
    }
}
