﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class VendorCategoryConfiguration : _BaseConfiguration<VendorCategory>
    {
        public override void Configure(EntityTypeBuilder<VendorCategory> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
