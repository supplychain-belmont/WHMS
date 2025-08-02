using System.ComponentModel;

namespace Indotalent.Domain.Enums;

public enum Status
{
    [Description("Borrador")] Draft = 0,
    [Description("Cancelado")] Cancelled = 1,
    [Description("Confirmado")] Confirmed = 2,
    [Description("Archivado")] Archived = 3
}
