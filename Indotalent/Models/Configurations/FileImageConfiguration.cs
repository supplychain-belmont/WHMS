using Indotalent.Domain.Entities;
using Indotalent.Persistence.Images;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations
{
    public class FileImageConfiguration : IEntityTypeConfiguration<FileImage>
    {
        public void Configure(EntityTypeBuilder<FileImage> builder)
        {
            builder.Property(f => f.OriginalFileName).HasMaxLength(255).IsRequired();
            builder.Property(f => f.CreatedAtUtc).IsRequired();
            builder.Property(f => f.CreatedByUserId).HasMaxLength(50);
            builder.Property(f => f.UpdatedAtUtc);
            builder.Property(f => f.UpdatedByUserId).HasMaxLength(50);
        }
    }
}

