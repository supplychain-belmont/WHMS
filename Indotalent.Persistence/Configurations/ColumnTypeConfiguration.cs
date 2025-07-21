using Indotalent.Domain.Grid;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations;

public class ColumnTypeConfiguration : _BaseConfiguration<ColumnType>
{
    public override void Configure(EntityTypeBuilder<ColumnType> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.TypeColumn).HasMaxLength(100);
        builder.Property(c => c.ForeignPath).HasMaxLength(100);
        builder.HasOne(c => c.Props)
            .WithMany()
            .HasForeignKey(c => c.PropsId)
            .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict);
    }
}
