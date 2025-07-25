﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class AdjustmentMinusConfiguration : _BaseConfiguration<AdjustmentMinus>
    {
        public override void Configure(EntityTypeBuilder<AdjustmentMinus> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
