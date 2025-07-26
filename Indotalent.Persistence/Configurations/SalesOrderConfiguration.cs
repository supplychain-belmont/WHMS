﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class SalesOrderConfiguration : _BaseConfiguration<SalesOrder>
    {
        public override void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.CustomerId).IsRequired();
            builder.Property(c => c.TaxId).IsRequired();
            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
        }
    }
}
