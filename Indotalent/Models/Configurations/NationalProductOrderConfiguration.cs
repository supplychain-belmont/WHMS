using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class NationalProductOrderConfiguration : _BaseConfiguration<NationalProductOrder>
    {
        public override void Configure(EntityTypeBuilder<NationalProductOrder> builder)
        {
            base.Configure(builder);

            builder.Property(n => n.Number).IsRequired().HasMaxLength(100);
            builder.Property(n => n.OrderDate).IsRequired();
            builder.Property(n => n.OrderStatus).IsRequired();
            builder.Property(n => n.Description).HasMaxLength(255);
            builder.Property(n => n.VendorId).IsRequired();
            builder.Property(n => n.TaxId).IsRequired();
            builder.Property(n => n.AmountPayable).IsRequired();
            builder.Property(n => n.TaxAmount).IsRequired();
            builder.Property(n => n.AfterTaxAmount).IsRequired();
            builder.Property(n => n.RowGuid).IsRequired();
            builder.Property(n => n.CreatedByUserId).IsRequired().HasMaxLength(50);
            builder.Property(n => n.CreatedAtUtc).IsRequired();
            builder.Property(n => n.UpdatedByUserId).IsRequired().HasMaxLength(50);
            builder.Property(n => n.UpdatedAtUtc).IsRequired();
            builder.Property(n => n.IsNotDeleted).IsRequired();
            builder.Property(n => n.PaymentId).IsRequired().HasMaxLength(50);
            builder.Property(n => n.AmountPaid).IsRequired();
            builder.Property(n => n.Balance).IsRequired();
            builder.Property(n => n.Invoice).IsRequired();
            builder.Property(n => n.FiscalCredit).IsRequired();
            builder.Property(n => n.PaymentID).IsRequired();
        }
    }
}
