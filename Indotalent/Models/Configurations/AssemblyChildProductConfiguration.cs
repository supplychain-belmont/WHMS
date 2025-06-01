using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations;

public class AssemblyChildProductConfiguration : _BaseConfiguration<AssemblyChild>
{
    public override void Configure(EntityTypeBuilder<AssemblyChild> builder)
    {
        base.Configure(builder);

        builder.HasOne(ac => ac.Product)
            .WithMany()
            .HasForeignKey(ac => ac.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
