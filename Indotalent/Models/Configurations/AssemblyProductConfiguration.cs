using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations;

public class AssemblyProductConfiguration : _BaseConfiguration<Assembly>
{
    public override void Configure(EntityTypeBuilder<Assembly> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(ap => ap.Product)
            .WithMany()
            .HasForeignKey(ap => ap.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
