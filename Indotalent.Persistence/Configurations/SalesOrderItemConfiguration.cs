﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class SalesOrderItemConfiguration : _BaseConfiguration<SalesOrderItem>
    {
        public override void Configure(EntityTypeBuilder<SalesOrderItem> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.SalesOrderId).IsRequired();
            builder.Property(c => c.ProductId).IsRequired();
            builder.Property(c => c.Summary).HasMaxLength(100);
        }
    }
}
