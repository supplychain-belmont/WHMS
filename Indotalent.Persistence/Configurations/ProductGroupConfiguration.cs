﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class ProductGroupConfiguration : _BaseConfiguration<ProductGroup>
    {
        public override void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
            builder.Property(c => c.CreatedByUserId).HasMaxLength(255);
            builder.Property(c => c.UpdatedByUserId).HasMaxLength(255);
            builder.Property(c => c.CreatedByUserId).HasMaxLength(255);
            builder.Property(c => c.CreatedByUserId).HasMaxLength(255);
        }
    }
}
