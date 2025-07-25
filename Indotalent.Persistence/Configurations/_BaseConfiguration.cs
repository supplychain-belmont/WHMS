﻿using Indotalent.Domain.Contracts;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public abstract class _BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IHasId, IHasAudit, IHasSoftDelete
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            ConfigureBaseProperties(builder);
        }

        protected virtual void ConfigureBaseProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.RowGuid).HasDefaultValue(Guid.NewGuid());
            builder.Property(e => e.CreatedByUserId).HasMaxLength(50);

            builder.Property(e => e.CreatedAtUtc)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : DateTime.MinValue
                );

            builder.Property(e => e.UpdatedByUserId).HasMaxLength(50);

            builder.Property(e => e.UpdatedAtUtc)
                .HasConversion(
                    v => v.HasValue ? v.Value.ToUniversalTime() : (DateTime?)null,
                    v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : DateTime.MinValue
                );

            builder.Property(e => e.IsNotDeleted).HasDefaultValue(true);
            builder.Property(e => e.CreatedByUserName).HasMaxLength(50);
            builder.Property(e => e.UpdatedByUserName).HasMaxLength(50);
        }
    }
}
