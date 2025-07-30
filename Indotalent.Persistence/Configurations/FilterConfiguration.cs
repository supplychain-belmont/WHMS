using Indotalent.Domain.Grid;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations;

public class FilterConfiguration : _BaseConfiguration<Filter>
{
    public override void Configure(EntityTypeBuilder<Filter> builder)
    {
        base.Configure(builder);
        builder.Property(f => f.Field).IsRequired().HasMaxLength(100);
        builder.Property(f => f.Operator).IsRequired();
        builder.Property(f => f.ValueRaw)
            .HasColumnName("Value");
    }
}
