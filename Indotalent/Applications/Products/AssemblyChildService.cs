using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Applications.Products;

public class AssemblyChildService : Repository<AssemblyChild>
{
    public AssemblyChildService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
