using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations;

public class AssemblyProductConfiguration : _BaseConfiguration<AssemblyProduct>
{
    public override void Configure(EntityTypeBuilder<AssemblyProduct> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(ap => ap.Assembly)
            .WithMany()
            .HasForeignKey(ap => ap.AssemblyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(ap => ap.Product)
            .WithMany()
            .HasForeignKey(ap => ap.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
