using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Grid;

public class ColumnType : _Base
{
    public string TypeColumn { get; set; }
    public string? ForeignPath { get; set; }
    public int? PropsId { get; set; }
    public ColumnModel? Props { get; set; }
    public int? GridId { get; set; }
    public Grid? Grid { get; set; }

    public List<Filter> Filters { get; set; } = new();
}
