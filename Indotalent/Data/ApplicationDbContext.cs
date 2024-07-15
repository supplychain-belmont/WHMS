using Indotalent.Infrastructures.Docs;
using Indotalent.Infrastructures.Images;
using Indotalent.Models.Configurations;
using Indotalent.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
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
        }
    }
}
