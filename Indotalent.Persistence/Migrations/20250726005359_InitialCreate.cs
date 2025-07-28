using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Indotalent.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdjustmentMinus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AdjustmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_AdjustmentMinus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdjustmentPlus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AdjustmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_AdjustmentPlus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Currency = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TimeZone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WhatsApp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TwitterX = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TikTok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
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
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_CustomerCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_CustomerGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileDocument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DocumentData = table.Column<byte[]>(type: "bytea", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalFileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ProductId = table.Column<int>(type: "integer", nullable: true),
                    ImageData = table.Column<byte[]>(type: "bytea", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "text", nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogAnalytic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IPAddress = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Url = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Device = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    GeographicLocation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Browser = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_LogAnalytic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogError",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExceptionMessage = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    StackTrace = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    AdditionalInfo = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
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
                    table.PrimaryKey("PK_LogError", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogSession",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IPAddress = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Action = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
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
                    table.PrimaryKey("PK_LogSession", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    ContainerM3 = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTransportContainerCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAgencyCost = table.Column<decimal>(type: "numeric", nullable: false),
                    LotDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    table.PrimaryKey("PK_Lots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationalProductOrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NationalProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UnitPrice = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    ManPowerCost = table.Column<float>(type: "real", nullable: false),
                    MaterialCost = table.Column<float>(type: "real", nullable: false),
                    ShippingCost = table.Column<float>(type: "real", nullable: false),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    DiscountCost = table.Column<float>(type: "real", nullable: false),
                    AmountPayable = table.Column<float>(type: "real", nullable: false),
                    Utility1 = table.Column<float>(type: "real", nullable: false),
                    Utility2 = table.Column<float>(type: "real", nullable: false),
                    UnitPriceInvoice = table.Column<float>(type: "real", nullable: false),
                    UnitPriceNoInvoice = table.Column<float>(type: "real", nullable: false),
                    ColorCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalProductOrderItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NationalProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    TaxId = table.Column<int>(type: "integer", nullable: false),
                    AmountPayable = table.Column<double>(type: "double precision", nullable: false),
                    TaxAmount = table.Column<double>(type: "double precision", nullable: false),
                    AfterTaxAmount = table.Column<double>(type: "double precision", nullable: false),
                    AmountPaid = table.Column<double>(type: "double precision", nullable: false),
                    Balance = table.Column<double>(type: "double precision", nullable: false),
                    Invoice = table.Column<bool>(type: "boolean", nullable: false),
                    FiscalCredit = table.Column<double>(type: "double precision", nullable: false),
                    PaymentID = table.Column<int>(type: "integer", nullable: false),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedByUserId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalProductOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumberSequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Prefix = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    Suffix = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    LastUsedCount = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_NumberSequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitMeasure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_UnitMeasure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_VendorCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VendorGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
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
                    table.PrimaryKey("PK_VendorGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SystemWarehouse = table.Column<bool>(type: "boolean", nullable: false),
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
                    table.PrimaryKey("PK_Warehouse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    JobTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Avatar = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UserType = table.Column<int>(type: "integer", nullable: false),
                    IsDefaultAdmin = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    SelectedCompanyId = table.Column<int>(type: "integer", maxLength: 50, nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Company_SelectedCompanyId",
                        column: x => x.SelectedCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WhatsApp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TwitterX = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TikTok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CustomerGroupId = table.Column<int>(type: "integer", nullable: false),
                    CustomerCategoryId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Customer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerCategory_CustomerCategoryId",
                        column: x => x.CustomerCategoryId,
                        principalTable: "CustomerCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customer_CustomerGroup_CustomerGroupId",
                        column: x => x.CustomerGroupId,
                        principalTable: "CustomerGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice40 = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice50 = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice60 = table.Column<decimal>(type: "numeric", nullable: true),
                    Physical = table.Column<bool>(type: "boolean", nullable: false),
                    IsAssembly = table.Column<bool>(type: "boolean", nullable: false),
                    AssemblyId = table.Column<int>(type: "integer", nullable: false),
                    UnitMeasureId = table.Column<int>(type: "integer", nullable: false),
                    ProductGroupId = table.Column<int>(type: "integer", nullable: false),
                    M3 = table.Column<decimal>(type: "numeric", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true),
                    ColorCode = table.Column<string>(type: "text", nullable: true),
                    Material = table.Column<string>(type: "text", nullable: true),
                    TapestryCode = table.Column<string>(type: "text", nullable: true),
                    IsNationalProduct = table.Column<bool>(type: "boolean", nullable: false),
                    ProductCategory = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_UnitMeasure_UnitMeasureId",
                        column: x => x.UnitMeasureId,
                        principalTable: "UnitMeasure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Street = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FaxNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Website = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WhatsApp = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Facebook = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TwitterX = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TikTok = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    VendorGroupId = table.Column<int>(type: "integer", nullable: false),
                    VendorCategoryId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Vendor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorCategory_VendorCategoryId",
                        column: x => x.VendorCategoryId,
                        principalTable: "VendorCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vendor_VendorGroup_VendorGroupId",
                        column: x => x.VendorGroupId,
                        principalTable: "VendorGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scrapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ScrappingDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Scrapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scrapping_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockCount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CountDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_StockCount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCount_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransferReleaseDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    WarehouseFromId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseToId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_TransferOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferOut_Warehouse_WarehouseFromId",
                        column: x => x.WarehouseFromId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransferOut_Warehouse_WarehouseToId",
                        column: x => x.WarehouseToId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_CustomerContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerContact_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OrderStatus = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    TaxId = table.Column<int>(type: "integer", nullable: false),
                    BeforeTaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    AfterTaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    FinalSaleAmount = table.Column<int>(type: "integer", nullable: true),
                    PaymentId = table.Column<string>(type: "text", nullable: true),
                    CreditDuesId = table.Column<int>(type: "integer", nullable: true),
                    CreditExistence = table.Column<bool>(type: "boolean", nullable: true),
                    Balance = table.Column<int>(type: "integer", nullable: true),
                    It = table.Column<float>(type: "real", nullable: true),
                    Invoice = table.Column<bool>(type: "boolean", nullable: false),
                    InvoicedAmount = table.Column<int>(type: "integer", nullable: true),
                    GrossProfit = table.Column<int>(type: "integer", nullable: true),
                    GrossMargin = table.Column<float>(type: "real", nullable: true),
                    DistributionMargin = table.Column<float>(type: "real", nullable: true),
                    Revenue = table.Column<float>(type: "real", nullable: true),
                    Commission = table.Column<float>(type: "real", nullable: true),
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
                    table.PrimaryKey("PK_SalesOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrder_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
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
                    table.PrimaryKey("PK_AssemblyProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssemblyProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ModuleId = table.Column<int>(type: "integer", nullable: false),
                    ModuleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ModuleCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    ModuleNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MovementDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    WarehouseId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    RequestedMovement = table.Column<decimal>(type: "numeric", nullable: false),
                    Movement = table.Column<decimal>(type: "numeric", nullable: false),
                    TransType = table.Column<int>(type: "integer", nullable: false),
                    Stock = table.Column<decimal>(type: "numeric", nullable: false),
                    WarehouseFromId = table.Column<int>(type: "integer", nullable: false),
                    WarehouseToId = table.Column<int>(type: "integer", nullable: false),
                    QtySCSys = table.Column<decimal>(type: "numeric", nullable: false),
                    QtySCCount = table.Column<decimal>(type: "numeric", nullable: false),
                    QtySCDelta = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_InventoryTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Warehouse_WarehouseFromId",
                        column: x => x.WarehouseFromId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Warehouse_WarehouseToId",
                        column: x => x.WarehouseToId,
                        principalTable: "Warehouse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LotItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LotId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCost = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostBrazil = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostDiscounted = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_LotItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LotItems_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LotItems_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    NationalProductOrderId = table.Column<int>(type: "integer", nullable: false),
                    Dimensions = table.Column<string>(type: "text", nullable: true),
                    Brand = table.Column<string>(type: "text", nullable: true),
                    Service = table.Column<string>(type: "text", nullable: true),
                    RowGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "text", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "text", nullable: true),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedByUserName = table.Column<string>(type: "text", nullable: true),
                    UpdatedByUserName = table.Column<string>(type: "text", nullable: true),
                    IsNotDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductDetails_NationalProductOrders_NationalProductOrderId",
                        column: x => x.NationalProductOrderId,
                        principalTable: "NationalProductOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ContainerM3 = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalTransportContainerCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalAgencyCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric", nullable: false),
                    OrderStatus = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
                    TaxId = table.Column<int>(type: "integer", nullable: false),
                    LotId = table.Column<int>(type: "integer", nullable: true),
                    BeforeTaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
                    AfterTaxAmount = table.Column<decimal>(type: "numeric", nullable: true),
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
                    table.PrimaryKey("PK_PurchaseOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Lots_LotId",
                        column: x => x.LotId,
                        principalTable: "Lots",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Tax_TaxId",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrder_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VendorContact",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    JobTitle = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    VendorId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_VendorContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VendorContact_Vendor_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransferIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TransferReceiveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    TransferOutId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_TransferIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransferIn_TransferOut_TransferOutId",
                        column: x => x.TransferOutId,
                        principalTable: "TransferOut",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeliveryDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    SalesOrderId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_DeliveryOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryOrder_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SalesOrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SalesOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UnitCost = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPrice40 = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice50 = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPrice60 = table.Column<decimal>(type: "numeric", nullable: true),
                    UnitPriceDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPriceDiscountPercentage = table.Column<decimal>(type: "numeric", nullable: false),
                    Commission = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossMargin = table.Column<decimal>(type: "numeric", nullable: false),
                    GrossContribution = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: true),
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
                    table.PrimaryKey("PK_SalesOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesOrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesOrderItem_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssemblyChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    AssemblyId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_AssemblyChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssemblyChild_AssemblyProduct_AssemblyId",
                        column: x => x.AssemblyId,
                        principalTable: "AssemblyProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssemblyChild_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GoodsReceive",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PurchaseOrderId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_GoodsReceive", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodsReceive_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PurchaseOrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    Summary = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UnitCost = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostBrazil = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostBolivia = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostBoliviaBs = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitCostDiscounted = table.Column<decimal>(type: "numeric", nullable: false),
                    TransportCost = table.Column<decimal>(type: "numeric", nullable: false),
                    AgencyCost = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalShippingCost = table.Column<decimal>(type: "numeric", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightedPercentageM3 = table.Column<decimal>(type: "numeric", nullable: false),
                    WeightedByCost = table.Column<decimal>(type: "numeric", nullable: false),
                    Total = table.Column<decimal>(type: "numeric", nullable: true),
                    AssemblyId = table.Column<int>(type: "integer", nullable: true),
                    IsAssembly = table.Column<bool>(type: "boolean", nullable: false),
                    ShowOrderItem = table.Column<bool>(type: "boolean", nullable: false),
                    LotItemId = table.Column<int>(type: "integer", nullable: true),
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
                    table.PrimaryKey("PK_PurchaseOrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_LotItems_LotItemId",
                        column: x => x.LotItemId,
                        principalTable: "LotItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SalesReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DeliveryOrderId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_SalesReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesReturn_DeliveryOrder_DeliveryOrderId",
                        column: x => x.DeliveryOrderId,
                        principalTable: "DeliveryOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseReturn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: true),
                    Description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    GoodsReceiveId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PurchaseReturn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseReturn_GoodsReceive_GoodsReceiveId",
                        column: x => x.GoodsReceiveId,
                        principalTable: "GoodsReceive",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_SelectedCompanyId",
                table: "AspNetUsers",
                column: "SelectedCompanyId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyChild_AssemblyId",
                table: "AssemblyChild",
                column: "AssemblyId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyChild_ProductId",
                table: "AssemblyChild",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_AssemblyProduct_ProductId",
                table: "AssemblyProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerCategoryId",
                table: "Customer",
                column: "CustomerCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CustomerGroupId",
                table: "Customer",
                column: "CustomerGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContact_CustomerId",
                table: "CustomerContact",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryOrder_SalesOrderId",
                table: "DeliveryOrder",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsReceive_PurchaseOrderId",
                table: "GoodsReceive",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_ProductId",
                table: "InventoryTransaction",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_WarehouseFromId",
                table: "InventoryTransaction",
                column: "WarehouseFromId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_WarehouseId",
                table: "InventoryTransaction",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_WarehouseToId",
                table: "InventoryTransaction",
                column: "WarehouseToId");

            migrationBuilder.CreateIndex(
                name: "IX_LotItems_LotId",
                table: "LotItems",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_LotItems_ProductId",
                table: "LotItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitMeasureId",
                table: "Product",
                column: "UnitMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_NationalProductOrderId",
                table: "ProductDetails",
                column: "NationalProductOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_LotId",
                table: "PurchaseOrder",
                column: "LotId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_TaxId",
                table: "PurchaseOrder",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_VendorId",
                table: "PurchaseOrder",
                column: "VendorId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_LotItemId",
                table: "PurchaseOrderItem",
                column: "LotItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_ProductId",
                table: "PurchaseOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderId",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseReturn_GoodsReceiveId",
                table: "PurchaseReturn",
                column: "GoodsReceiveId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_CustomerId",
                table: "SalesOrder",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_TaxId",
                table: "SalesOrder",
                column: "TaxId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_ProductId",
                table: "SalesOrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderItem_SalesOrderId",
                table: "SalesOrderItem",
                column: "SalesOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_DeliveryOrderId",
                table: "SalesReturn",
                column: "DeliveryOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Scrapping_WarehouseId",
                table: "Scrapping",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCount_WarehouseId",
                table: "StockCount",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferIn_TransferOutId",
                table: "TransferIn",
                column: "TransferOutId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_WarehouseFromId",
                table: "TransferOut",
                column: "WarehouseFromId");

            migrationBuilder.CreateIndex(
                name: "IX_TransferOut_WarehouseToId",
                table: "TransferOut",
                column: "WarehouseToId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorCategoryId",
                table: "Vendor",
                column: "VendorCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_VendorGroupId",
                table: "Vendor",
                column: "VendorGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_VendorContact_VendorId",
                table: "VendorContact",
                column: "VendorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdjustmentMinus");

            migrationBuilder.DropTable(
                name: "AdjustmentPlus");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssemblyChild");

            migrationBuilder.DropTable(
                name: "CustomerContact");

            migrationBuilder.DropTable(
                name: "FileDocument");

            migrationBuilder.DropTable(
                name: "FileImages");

            migrationBuilder.DropTable(
                name: "InventoryTransaction");

            migrationBuilder.DropTable(
                name: "LogAnalytic");

            migrationBuilder.DropTable(
                name: "LogError");

            migrationBuilder.DropTable(
                name: "LogSession");

            migrationBuilder.DropTable(
                name: "NationalProductOrderItems");

            migrationBuilder.DropTable(
                name: "NumberSequence");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem");

            migrationBuilder.DropTable(
                name: "PurchaseReturn");

            migrationBuilder.DropTable(
                name: "SalesOrderItem");

            migrationBuilder.DropTable(
                name: "SalesReturn");

            migrationBuilder.DropTable(
                name: "Scrapping");

            migrationBuilder.DropTable(
                name: "StockCount");

            migrationBuilder.DropTable(
                name: "TransferIn");

            migrationBuilder.DropTable(
                name: "VendorContact");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AssemblyProduct");

            migrationBuilder.DropTable(
                name: "NationalProductOrders");

            migrationBuilder.DropTable(
                name: "LotItems");

            migrationBuilder.DropTable(
                name: "GoodsReceive");

            migrationBuilder.DropTable(
                name: "DeliveryOrder");

            migrationBuilder.DropTable(
                name: "TransferOut");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "PurchaseOrder");

            migrationBuilder.DropTable(
                name: "SalesOrder");

            migrationBuilder.DropTable(
                name: "Warehouse");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropTable(
                name: "UnitMeasure");

            migrationBuilder.DropTable(
                name: "Lots");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "VendorCategory");

            migrationBuilder.DropTable(
                name: "VendorGroup");

            migrationBuilder.DropTable(
                name: "CustomerCategory");

            migrationBuilder.DropTable(
                name: "CustomerGroup");
        }
    }
}
