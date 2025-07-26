﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class GoodsReceiveConfiguration : _BaseConfiguration<GoodsReceive>
    {
        public override void Configure(EntityTypeBuilder<GoodsReceive> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.PurchaseOrderId).IsRequired();
            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
