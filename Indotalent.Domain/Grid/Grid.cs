using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Grid;

public class Grid : _Base
{
    public string Name { get; set; }
    public List<ColumnType> ColumnTypes { get; set; } = new();
}
