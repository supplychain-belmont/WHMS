using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Indotalent.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = string.Empty;

            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                query = @"
                    DROP VIEW IF EXISTS ""InventoryStock"";
                    CREATE VIEW ""InventoryStock"" AS
                    SELECT
                        t.""WarehouseId"",
                        t.""ProductId"",
                        w.""Name"" AS ""Warehouse"",
                        p.""Name"" AS ""Product"",
                        SUM(CASE WHEN t.""Status"" >= 2 THEN t.""Stock"" ELSE 0 END) AS ""Stock"",
                        SUM(CASE WHEN t.""Status"" = 0 AND t.""TransType"" = -1 THEN t.""Movement"" ELSE 0 END) AS ""Reserved"",
                        SUM(CASE WHEN t.""Status"" = 0 AND t.""TransType"" = 1 THEN t.""Movement"" ELSE 0 END) AS ""Incoming"",
                        MAX(t.""Id"") AS ""Id"",
                        MAX(t.""RowGuid""::TEXT) AS ""RowGuid"", -- Cast UUID for PostgreSQL
                        MAX(t.""CreatedAtUtc"") AS ""CreatedAtUtc"",
                        MAX(t.""CreatedByUserId"") AS ""CreatedByUserId"",
                        MAX(t.""UpdatedAtUtc"") AS ""UpdatedAtUtc"",
                        MAX(t.""UpdatedByUserId"") AS ""UpdatedByUserId"",
                        CAST(1 AS BOOLEAN) AS ""IsNotDeleted""
                    FROM ""InventoryTransaction"" t
                    JOIN ""Warehouse"" w ON t.""WarehouseId"" = w.""Id""
                    JOIN ""Product"" p ON t.""ProductId"" = p.""Id""
                    WHERE (t.""Status"" >= 2 OR t.""Status"" = 0) 
                        AND w.""SystemWarehouse"" = FALSE 
                        AND p.""Physical"" = TRUE 
                        AND t.""IsNotDeleted"" = TRUE
                    GROUP BY t.""WarehouseId"", t.""ProductId"", w.""Name"", p.""Name"";
                 ";
                migrationBuilder.Sql(query);
            }
            else
            {
                query = @"
                CREATE OR ALTER VIEW InventoryStock AS
                SELECT
                    t.WarehouseId,
                    t.ProductId,
                    w.Name AS Warehouse,
                    p.Name AS Product,
                    SUM(CASE WHEN t.Status >= 2 THEN t.Stock ELSE 0 END) AS Stock,
                    SUM(CASE WHEN t.Status = 0 AND t.TransType = -1 THEN t.Movement ELSE 0 END) AS Reserved,
                    SUM(CASE WHEN t.Status = 0 AND t.TransType = 1 THEN t.Movement ELSE 0 END) AS Incoming,
                    MAX(t.Id) AS Id,
                    MAX(t.RowGuid) AS RowGuid,
                    MAX(t.CreatedAtUtc) AS CreatedAtUtc,
                    MAX(t.CreatedByUserId) AS CreatedByUserId,
                    MAX(t.UpdatedAtUtc) AS UpdatedAtUtc,
                    MAX(t.UpdatedByUserId) AS UpdatedByUserId,
                    CAST(1 AS BIT) AS IsNotDeleted
                FROM InventoryTransaction t
                JOIN Warehouse w ON t.WarehouseId = w.Id
                JOIN Product p ON t.ProductId = p.Id
                WHERE (t.Status >= 2 OR t.Status = 0) AND w.SystemWarehouse = 0 AND p.Physical = 1 AND t.IsNotDeleted = 1
                GROUP BY t.WarehouseId, t.ProductId, w.Name, p.Name";
                migrationBuilder.Sql(query);
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder.ActiveProvider == "Npgsql.EntityFrameworkCore.PostgreSQL")
            {
                migrationBuilder.Sql(@"DROP VIEW IF EXISTS ""InventoryStock"";");
            }
            else
            {
                migrationBuilder.Sql(@"IF OBJECT_ID('InventoryStock', 'V') IS NOT NULL
                DROP VIEW InventoryStock;");
            }
        }
    }
}
