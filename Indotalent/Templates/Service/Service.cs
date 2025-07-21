using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Templates.Service;

public class _BaseService : Repository<_Base>
{
    public _BaseService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
