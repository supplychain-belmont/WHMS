﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class TransferInConfiguration : _BaseConfiguration<TransferIn>
    {
        public override void Configure(EntityTypeBuilder<TransferIn> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.TransferOutId).IsRequired();
            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
