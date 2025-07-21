using Indotalent.Domain.Contracts;
using Indotalent.Models.Configurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Indotalent.Templates.Configuration;

#if (!EnableEntityClass)
public abstract class __Entity__ : _Base
{
}
#endif

public class __Entity__Configuration : _BaseConfiguration<__Entity__>
{
    public override void Configure(EntityTypeBuilder<__Entity__> builder)
    {
        base.Configure(builder);
    }
}
