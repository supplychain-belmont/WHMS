﻿using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configurations
{
    public class VendorConfiguration : _BaseConfiguration<Vendor>
    {
        public override void Configure(EntityTypeBuilder<Vendor> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.VendorGroupId).IsRequired();
            builder.Property(c => c.VendorCategoryId).IsRequired();
            builder.Property(c => c.Number).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(255);
            builder.Property(c => c.Street).HasMaxLength(100);
            builder.Property(c => c.City).HasMaxLength(100);
            builder.Property(c => c.State).HasMaxLength(100);
            builder.Property(c => c.ZipCode).HasMaxLength(100);
            builder.Property(c => c.Country).HasMaxLength(100);
            builder.Property(c => c.PhoneNumber).HasMaxLength(20);
            builder.Property(c => c.FaxNumber).HasMaxLength(20);
            builder.Property(c => c.EmailAddress).HasMaxLength(100);
            builder.Property(c => c.Website).HasMaxLength(100);
            builder.Property(c => c.WhatsApp).HasMaxLength(20);
            builder.Property(c => c.LinkedIn).HasMaxLength(100);
            builder.Property(c => c.Facebook).HasMaxLength(100);
            builder.Property(c => c.Instagram).HasMaxLength(100);
            builder.Property(c => c.TwitterX).HasMaxLength(100);
            builder.Property(c => c.TikTok).HasMaxLength(100);
        }
    }
}
