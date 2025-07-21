using Indotalent.Domain.Contracts;
using Indotalent.Domain.Grid;
using Indotalent.Persistence.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Persistence.Configuration;

public class GridConfiguration : _BaseConfiguration<Grid>
{
    public override void Configure(EntityTypeBuilder<Grid> builder)
    {
        base.Configure(builder);
    }
}
