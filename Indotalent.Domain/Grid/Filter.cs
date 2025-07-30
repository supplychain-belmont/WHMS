using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

using Indotalent.Domain.Contracts;
using Indotalent.Domain.Enums;

namespace Indotalent.Domain.Grid;

public class Filter : _Base
{
    public string Field { get; set; }
    public Operator Operator { get; set; }

    [NotMapped]
    public object? Value
    {
        get => string.IsNullOrWhiteSpace(ValueRaw)
            ? null
            : JsonSerializer.Deserialize<object>(ValueRaw);
        set => ValueRaw = value == null
            ? null
            : JsonSerializer.Serialize(value);
    }

    public string? ValueRaw { get; set; } // Mapeada por EF
    public int ColumnTypeId { get; set; }
    public ColumnType? ColumnType { get; set; }
}
