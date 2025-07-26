﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class ScrappingConfiguration : _BaseConfiguration<Scrapping>
    {
        public override void Configure(EntityTypeBuilder<Scrapping> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.WarehouseId).IsRequired();
            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
