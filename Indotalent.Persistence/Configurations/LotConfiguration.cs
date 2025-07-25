using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations;

public class LotConfiguration : _BaseConfiguration<Lot>
{
    public override void Configure(EntityTypeBuilder<Lot> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Number).HasMaxLength(100);
        builder.Property(c => c.Name).HasMaxLength(255);

    }
}
