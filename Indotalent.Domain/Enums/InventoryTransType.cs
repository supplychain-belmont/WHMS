using System.ComponentModel;

namespace Indotalent.Domain.Enums
{
    public enum InventoryTransType
    {
        [Description("In")]
        In = 1,
        [Description("Out")]
        Out = -1,
    }
}
