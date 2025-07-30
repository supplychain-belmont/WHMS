using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Indotalent.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GridCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ColumnModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Field = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Uid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Index = table.Column<int>(type: "integer", nullable: true),
                    HeaderText = table.Column<string>(type: "text", nullable: true),
                    Width = table.Column<int>(type: "integer", nullable: false),
                    MinWidth = table.Column<int>(type: "integer", nullable: false),
                    MaxWidth = table.Column<int>(type: "integer", nullable: false),
                    TextAlign = table.Column<int>(type: "integer", nullable: true),
                    ClipMode = table.Column<int>(type: "integer", nullable: true),
                    HeaderTextAlign = table.Column<int>(type: "integer", nullable: true),
                    DisableHtmlEncode = table.Column<bool>(type: "boolean", nullable: true),
                    Type = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Format = table.Column<string>(type: "text", nullable: true),
                    Visible = table.Column<bool>(type: "boolean", nullable: true),
                    Template = table.Column<string>(type: "text", nullable: true),
                    HeaderTemplate = table.Column<string>(type: "text", nullable: true),
                    IsFrozen = table.Column<bool>(type: "boolean", nullable: true),
                    AllowSorting = table.Column<bool>(type: "boolean", nullable: true),
                    AllowResizing = table.Column<bool>(type: "boolean", nullable: true),
                    ShowColumnMenu = table.Column<bool>(type: "boolean", nullable: true),
                    AllowFiltering = table.Column<bool>(type: "boolean", nullable: true),
                    AllowGrouping = table.Column<bool>(type: "boolean", nullable: true),
                    AllowReordering = table.Column<bool>(type: "boolean", nullable: true),
                    EnableGroupByFormat = table.Column<bool>(type: "boolean", nullable: true),
                    AllowEditing = table.Column<bool>(type: "boolean", nullable: true),
                    DisplayAsCheckBox = table.Column<bool>(type: "boolean", nullable: true),
                    IsPrimaryKey = table.Column<bool>(type: "boolean", nullable: true),
                    EditType = table.Column<string>(type: "text", nullable: true),
                    DefaultValue = table.Column<string>(type: "text", nullable: true),
                    IsIdentity = table.Column<bool>(type: "boolean", nullable: true),
                    ForeignKeyField = table.Column<string>(type: "text", nullable: true),
                    ForeignKeyValue = table.Column<string>(type: "text", nullable: true),
                    HideAtMedia = table.Column<string>(type: "text", nullable: true),
                    ShowInColumnChooser = table.Column<bool>(type: "boolean", nullable: true),
                    CommandsTemplate = table.Column<string>(type: "text", nullable: true),
                    SortComparer = table.Column<string>(type: "text", nullable: true),
                    EditTemplate = table.Column<string>(type: "text", nullable: true),
                    FilterTemplate = table.Column<string>(type: "text", nullable: true),
                    LockColumn = table.Column<bool>(type: "boolean", nullable: true),
                    AllowSearching = table.Column<bool>(type: "boolean", nullable: true),
                    AutoFit = table.Column<bool>(type: "boolean", nullable: true),
                    Freeze = table.Column<int>(type: "integer", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grids", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColumnTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeColumn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ForeignPath = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PropsId = table.Column<int>(type: "integer", nullable: true),
                    GridId = table.Column<int>(type: "integer", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ColumnTypes_ColumnModels_PropsId",
                        column: x => x.PropsId,
                        principalTable: "ColumnModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ColumnTypes_Grids_GridId",
                        column: x => x.GridId,
                        principalTable: "Grids",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Filters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Field = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Operator = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    ColumnTypeId = table.Column<int>(type: "integer", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filters_ColumnTypes_ColumnTypeId",
                        column: x => x.ColumnTypeId,
                        principalTable: "ColumnTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColumnTypes_GridId",
                table: "ColumnTypes",
                column: "GridId");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnTypes_PropsId",
                table: "ColumnTypes",
                column: "PropsId");

            migrationBuilder.CreateIndex(
                name: "IX_Filters_ColumnTypeId",
                table: "Filters",
                column: "ColumnTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filters");

            migrationBuilder.DropTable(
                name: "ColumnTypes");

            migrationBuilder.DropTable(
                name: "ColumnModels");

            migrationBuilder.DropTable(
                name: "Grids");
        }
    }
}
