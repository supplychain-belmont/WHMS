using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class CreditDuesConfiguration : _BaseConfiguration<CreditDues>
    {
        public override void Configure(EntityTypeBuilder<CreditDues> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.CreditId).IsRequired();
            builder.Property(c => c.FileImage).HasMaxLength(255);
            builder.Property(c => c.InitialDate).IsRequired();
            builder.Property(c => c.FinalDate).IsRequired();
            builder.Property(c => c.CustomerId).IsRequired();
            builder.Property(c => c.DueValue).IsRequired();
            builder.Property(c => c.DueNumber).IsRequired();
            builder.Property(c => c.DueLapse).IsRequired();
            builder.Property(c => c.InitialPaymentPercentage).IsRequired();
            builder.Property(c => c.PaymentStatus).IsRequired();
        }
    }
}
