using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class ProductDetailsConfiguration : _BaseConfiguration<ProductDetails>
    {
        public override void Configure(EntityTypeBuilder<ProductDetails> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Dimensions).HasMaxLength(255);
            builder.Property(c => c.Brand).HasMaxLength(100);
            builder.Property(c => c.Service).HasMaxLength(255);

            builder.HasOne(c => c.Product)
                .WithMany()
                .HasForeignKey(c => c.ProductId)
                .IsRequired();

            builder.HasOne(c => c.NationalProductOrder)
                .WithMany()
                .HasForeignKey(c => c.NationalProductOrderId)
                .IsRequired();
        }
    }
}
