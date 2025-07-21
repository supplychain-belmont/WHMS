using Indotalent.Domain.Grid;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations;

public class ColumnModelConfiguration : _BaseConfiguration<ColumnModel>
{
    public override void Configure(EntityTypeBuilder<ColumnModel> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Field).HasMaxLength(100);
        builder.Property(c => c.Uid)
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Property(c => c.Type)
            .HasMaxLength(100)
            .IsRequired(false);
        builder.Ignore(c => c.CustomAttributes);
        builder.Ignore(c => c.ValidationRules);
        builder.Property(c => c.DefaultValueRaw)
            .HasColumnName("DefaultValue");
    }
}
