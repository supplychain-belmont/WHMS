using Indotalent.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class PaymentConfiguration : _BaseConfiguration<Payment>
    {
        public override void Configure(EntityTypeBuilder<Payment> builder)
        {
            base.Configure(builder);

            builder.Property(p => p.PaymentName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Id).IsRequired();
        }
    }
}