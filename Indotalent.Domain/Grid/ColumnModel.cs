using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Grid;

public enum TextAlign
{
    Left,
    Right,
    Center,
    Justify
}

public enum ClipMode
{
    Clip,
    Ellipsis,
    EllipsisWithTooltip
}

public enum FreezeDirection
{
    None,
    Left,
    Right,
    Fixed
}

public class ColumnModel : _Base
{
    public string? Field { get; set; }
    public string? Uid { get; set; }
    public int? Index { get; set; }
    public string? HeaderText { get; set; }
    public int Width { get; set; }
    public int MinWidth { get; set; }
    public int MaxWidth { get; set; }

    public TextAlign? TextAlign { get; set; }
    public ClipMode? ClipMode { get; set; }
    public TextAlign? HeaderTextAlign { get; set; }

    public bool? DisableHtmlEncode { get; set; }
    public string? Type { get; set; }
    public string? Format { get; set; }

    public bool? Visible { get; set; } = true;
    public string? Template { get; set; }
    public string? HeaderTemplate { get; set; }

    public bool? IsFrozen { get; set; }
    public bool? AllowSorting { get; set; } = true;
    public bool? AllowResizing { get; set; } = true;
    public bool? ShowColumnMenu { get; set; } = true;
    public bool? AllowFiltering { get; set; } = true;
    public bool? AllowGrouping { get; set; } = true;
    public bool? AllowReordering { get; set; } = true;
    public bool? EnableGroupByFormat { get; set; } = false;
    public bool? AllowEditing { get; set; } = true;

    public Dictionary<string, object>? CustomAttributes { get; set; }
    public bool? DisplayAsCheckBox { get; set; }
    public bool? IsPrimaryKey { get; set; } = false;
    public string? EditType { get; set; }

    public Dictionary<string, object>? ValidationRules { get; set; }

    [NotMapped]
    public object? DefaultValue
    {
        get => string.IsNullOrWhiteSpace(DefaultValueRaw)
            ? null
            : JsonSerializer.Deserialize<object>(DefaultValueRaw);
        set => DefaultValueRaw = value == null
            ? null
            : JsonSerializer.Serialize(value);
    }

    public string? DefaultValueRaw { get; set; }

    public bool? IsIdentity { get; set; } = false;

    public string? ForeignKeyField { get; set; }
    public string? ForeignKeyValue { get; set; }
    public string? HideAtMedia { get; set; }
    public bool? ShowInColumnChooser { get; set; } = true;

    public string? CommandsTemplate { get; set; }
    public string? SortComparer { get; set; }
    public string? EditTemplate { get; set; }
    public string? FilterTemplate { get; set; }

    public bool? LockColumn { get; set; }
    public bool? AllowSearching { get; set; } = true;
    public bool? AutoFit { get; set; } = false;

    public FreezeDirection? Freeze { get; set; }
}
