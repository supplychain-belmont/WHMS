using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Models.Configurations;

public class LotItemConfiguration : _BaseConfiguration<LotItem>
{
    public override void Configure(EntityTypeBuilder<LotItem> builder)
    {
        base.Configure(builder);

        builder.HasOne(li => li.Product)
            .WithMany()
            .HasForeignKey(li => li.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
