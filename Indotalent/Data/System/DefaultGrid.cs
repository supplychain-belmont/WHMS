using Indotalent.Domain.Grid;
using Indotalent.Templates.Service;
using Indotalent.Utils.Columns;

namespace Indotalent.Data.System;

public class DefaultGrid
{
    public static async Task GenerateAsync(IServiceProvider services)
    {
        var gridService = services.GetRequiredService<GridService>();

        var purchaseGrid = new Grid
        {
            Name = "PurchaseOrder",
            ColumnTypes = new List<ColumnType>
            {
                DefaultColumn.GenerateCheckBox(),
                DefaultColumn.GenerateIdColumnType(),
                DefaultColumn.GenerateCodeNumber()
            }
        };

        await gridService.AddAsync(purchaseGrid);
    }
}
