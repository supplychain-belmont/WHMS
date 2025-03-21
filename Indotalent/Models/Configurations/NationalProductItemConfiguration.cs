using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class NationalProductOrderItemConfiguration : _BaseConfiguration<NationalProductOrderItem>
    {
        public override void Configure(EntityTypeBuilder<NationalProductOrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(n => n.NationalProductOrderId).IsRequired();
            builder.Property(n => n.ProductId).IsRequired();
            builder.Property(n => n.Summary).HasMaxLength(255);
            builder.Property(n => n.UnitPrice).IsRequired();
            builder.Property(n => n.Quantity).IsRequired();
            builder.Property(n => n.Total).IsRequired();
            builder.Property(n => n.RowGuid).IsRequired();
            builder.Property(n => n.CreatedByUserId).HasMaxLength(100);
            builder.Property(n => n.CreatedAtUtc).IsRequired();
            builder.Property(n => n.UpdatedByUserId).HasMaxLength(100);
            builder.Property(n => n.UpdatedAtUtc).IsRequired();
            builder.Property(n => n.IsNotDeleted).IsRequired();
            builder.Property(n => n.ManPowerCost).IsRequired();
            builder.Property(n => n.MaterialCost).IsRequired();
            builder.Property(n => n.ShippingCost).IsRequired();
            builder.Property(n => n.TotalAmount).IsRequired();
            builder.Property(n => n.DiscountCost).IsRequired();
            builder.Property(n => n.AmountPayable).IsRequired();
            builder.Property(n => n.Utility1).IsRequired();
            builder.Property(n => n.Utility2).IsRequired();
            builder.Property(n => n.UnitPriceInvoice).IsRequired();
            builder.Property(n => n.UnitPriceNoInvoice).IsRequired();
            builder.Property(n => n.ColorCode).HasMaxLength(50);
        }
    }
}
