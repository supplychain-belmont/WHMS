using System.Runtime.CompilerServices;

using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Docs;
using Indotalent.Infrastructures.Images;
using Indotalent.Models.Configurations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public virtual DbSet<FileImage> FileImages { get; set; } = default!;
        public virtual DbSet<FileDocument> FileDocument { get; set; } = default!;
        public virtual DbSet<Company> Company { get; set; } = default!;
        public virtual DbSet<NumberSequence> NumberSequence { get; set; } = default!;
        public virtual DbSet<LogSession> LogSession { get; set; } = default!;
        public virtual DbSet<LogError> LogError { get; set; } = default!;
        public virtual DbSet<LogAnalytic> LogAnalytic { get; set; } = default!;
        public virtual DbSet<CustomerGroup> CustomerGroup { get; set; } = default!;
        public virtual DbSet<CustomerCategory> CustomerCategory { get; set; } = default!;
        public virtual DbSet<VendorGroup> VendorGroup { get; set; } = default!;
        public virtual DbSet<VendorCategory> VendorCategory { get; set; } = default!;
        public virtual DbSet<Warehouse> Warehouse { get; set; } = default!;
        public virtual DbSet<Customer> Customer { get; set; } = default!;
        public virtual DbSet<Vendor> Vendor { get; set; } = default!;
        public virtual DbSet<UnitMeasure> UnitMeasure { get; set; } = default!;
        public virtual DbSet<ProductGroup> ProductGroup { get; set; } = default!;
        public virtual DbSet<Product> Product { get; set; } = default!;
        public virtual DbSet<CustomerContact> CustomerContact { get; set; } = default!;
        public virtual DbSet<VendorContact> VendorContact { get; set; } = default!;
        public virtual DbSet<Tax> Tax { get; set; } = default!;
        public virtual DbSet<SalesOrder> SalesOrder { get; set; } = default!;
        public virtual DbSet<SalesOrderItem> SalesOrderItem { get; set; } = default!;
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; } = default!;
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItem { get; set; } = default!;
        public virtual DbSet<InventoryTransaction> InventoryTransaction { get; set; } = default!;
        public virtual DbSet<DeliveryOrder> DeliveryOrder { get; set; } = default!;
        public virtual DbSet<GoodsReceive> GoodsReceive { get; set; } = default!;
        public virtual DbSet<SalesReturn> SalesReturn { get; set; } = default!;
        public virtual DbSet<PurchaseReturn> PurchaseReturn { get; set; } = default!;
        public virtual DbSet<TransferIn> TransferIn { get; set; } = default!;
        public virtual DbSet<TransferOut> TransferOut { get; set; } = default!;
        public virtual DbSet<StockCount> StockCount { get; set; } = default!;
        public virtual DbSet<AdjustmentMinus> AdjustmentMinus { get; set; } = default!;
        public virtual DbSet<AdjustmentPlus> AdjustmentPlus { get; set; } = default!;
        public virtual DbSet<Scrapping> Scrapping { get; set; } = default!;
        public virtual DbSet<NationalProductOrder> NationalProductOrders { get; set; } = default!;
        public virtual DbSet<NationalProductOrderItem> NationalProductOrderItems { get; set; } = default!;
        public virtual DbSet<ProductDetails> ProductDetails { get; set; } = default!;
        public virtual DbSet<AssemblyProduct> AssemblyProduct { get; set; } = default!;
        public virtual DbSet<Lot> Lots { get; set; } = default!;
        public virtual DbSet<LotItem> LotItems { get; set; } = default!;
        public virtual DbSet<InventoryStock> InventoryStocks { get; set; } = default!;

        public virtual void SetModifiedState(object entity) =>
            Entry(entity).State = EntityState.Modified;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileImage>().HasKey(f => f.Id);
            modelBuilder.Entity<FileImage>().Property(f => f.OriginalFileName).HasMaxLength(100);
            modelBuilder.Entity<FileDocument>().HasKey(f => f.Id);
            modelBuilder.Entity<FileDocument>().Property(f => f.OriginalFileName).HasMaxLength(100);

            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new LogAnalyticConfiguration());
            modelBuilder.ApplyConfiguration(new LogErrorConfiguration());
            modelBuilder.ApplyConfiguration(new LogSessionConfiguration());
            modelBuilder.ApplyConfiguration(new NumberSequenceConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerGroupConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new VendorGroupConfiguration());
            modelBuilder.ApplyConfiguration(new VendorCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new WarehouseConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new VendorConfiguration());
            modelBuilder.ApplyConfiguration(new UnitMeasureConfiguration());
            modelBuilder.ApplyConfiguration(new ProductGroupConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerContactConfiguration());
            modelBuilder.ApplyConfiguration(new VendorContactConfiguration());
            modelBuilder.ApplyConfiguration(new TaxConfiguration());
            modelBuilder.ApplyConfiguration(new SalesOrderConfiguration());
            modelBuilder.ApplyConfiguration(new SalesOrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryOrderConfiguration());
            modelBuilder.ApplyConfiguration(new GoodsReceiveConfiguration());
            modelBuilder.ApplyConfiguration(new SalesReturnConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseReturnConfiguration());
            modelBuilder.ApplyConfiguration(new TransferInConfiguration());
            modelBuilder.ApplyConfiguration(new TransferOutConfiguration());
            modelBuilder.ApplyConfiguration(new StockCountConfiguration());
            modelBuilder.ApplyConfiguration(new AdjustmentMinusConfiguration());
            modelBuilder.ApplyConfiguration(new AdjustmentPlusConfiguration());
            modelBuilder.ApplyConfiguration(new ScrappingConfiguration());
            modelBuilder.ApplyConfiguration(new NationalProductOrderConfiguration());
            modelBuilder.ApplyConfiguration(new NationalProductOrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new AssemblyProductConfiguration());
            modelBuilder.ApplyConfiguration(new LotConfiguration());
            modelBuilder.ApplyConfiguration(new LotItemConfiguration());

            modelBuilder.Entity<InventoryStock>()
                .ToView("InventoryStock")
                .HasKey(v => v.Id);
        }

        public Task CreateInventoryStockView()
        {
            Console.WriteLine("Database Provider: " + Database.ProviderName);
            var query = "";
            if (IsPostgresSql)
            {
                query = @"
                    DROP VIEW IF EXISTS InventoryStock;
                    CREATE VIEW InventoryStock AS
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
                return Database.ExecuteSqlRawAsync(query);
            }

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
                    CAST(1 AS BIT) AS IsNotDelete
                FROM InventoryTransaction t
                JOIN Warehouse w ON t.WarehouseId = w.Id
                JOIN Product p ON t.ProductId = p.Id
                WHERE (t.Status >= 2 OR t.Status = 0) AND w.SystemWarehouse = 0 AND p.Physical = 1 AND t.IsNotDeleted = 1
                GROUP BY t.WarehouseId, t.ProductId, w.Name, p.Name";
            return Database.ExecuteSqlRawAsync(query);
        }

        private bool IsPostgresSql => Database.ProviderName == "Npgsql.EntityFrameworkCore.PostgreSQL";
    }
}
