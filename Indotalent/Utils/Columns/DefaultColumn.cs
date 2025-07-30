using Indotalent.Domain.Grid;

namespace Indotalent.Utils.Columns;

public static class DefaultColumn
{
    public static ColumnType GenerateCheckBox()
    {
        return new ColumnType
        {
            TypeColumn = "checkbox",
            Props = new ColumnModel { Type = "checkbox", Width = 50, Freeze = FreezeDirection.None }
        };
    }

    public static ColumnType GenerateCodeNumber(string field = "Number")
    {
        return new ColumnType
        {
            TypeColumn = "text",
            Props = new ColumnModel()
            {
                Field = field,
                HeaderText = "Código",
                AllowEditing = false,
                Type = "string"
            }
        };
    }

    public static ColumnType GenerateRowGuidColumnType()
    {
        return new ColumnType
        {
            TypeColumn = "key",
            Props = new ColumnModel()
            {
                Field = "RowGuid",
                HeaderText = "Identificador",
                Visible = false,
                AllowEditing = false,
                IsPrimaryKey = true,
            }
        };
    }

    public static ColumnType GenerateIdColumnType()
    {
        return new ColumnType
        {
            TypeColumn = "key",
            Props = new ColumnModel
            {
                Field = "Id",
                HeaderText = "Identificador",
                Visible = false,
                AllowEditing = false,
                IsPrimaryKey = true
            }
        };
    }

    public static ColumnType GenerateDateColumnType(bool visible = true)
    {
        return new ColumnType()
        {
            TypeColumn = "date",
            Props = new ColumnModel()
            {
                Field = "CreatedAtUtc",
                HeaderText = "Fecha de creación",
                EditType = "datetimepickeredit",
                AllowEditing = false,
                Visible = visible,
            }
        };
    }

    public static ColumnType GenerateForeignColumn(string field, string headerText, string foreignKeyField,
        string foreignKeyValue, string typeColumn = "text", FreezeDirection freeze = FreezeDirection.None)
    {
        return new ColumnType()
        {
            TypeColumn = typeColumn,
            Props = new ColumnModel()
            {
                Type = typeColumn,
                HeaderText = headerText,
                Field = field,
                MinWidth = 250,
                ForeignKeyField = foreignKeyField,
                ForeignKeyValue = foreignKeyValue,
                EditType = "dropdownedit",
                Freeze = freeze,
                AutoFit = true,
            }
        };
    }
}
