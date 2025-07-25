﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class VendorGroupConfiguration : _BaseConfiguration<VendorGroup>
    {
        public override void Configure(EntityTypeBuilder<VendorGroup> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
