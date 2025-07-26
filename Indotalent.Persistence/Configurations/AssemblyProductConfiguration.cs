using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations;

public class AssemblyProductConfiguration : _BaseConfiguration<Assembly>
{
    public override void Configure(EntityTypeBuilder<Assembly> builder)
    {
        base.Configure(builder);

        builder.HasOne(ac => ac.Product)
            .WithMany()
            .HasForeignKey(ac => ac.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(c => c.Description)
            .HasMaxLength(1000)
            .IsRequired(false);
    }
}
