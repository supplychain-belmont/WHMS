using System.ComponentModel;

namespace Indotalent.Domain.Enums
{
    public enum UserType
    {
        [Description("Internal")]
        Internal = 0,
        [Description("Customer")]
        Customer = 1,
        [Description("Vendor")]
        Vendor = 2
    }
}
