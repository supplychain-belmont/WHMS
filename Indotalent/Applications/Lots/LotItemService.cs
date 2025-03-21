using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

namespace Indotalent.Applications.Lots;

public class LotItemService : Repository<LotItem>
{
    public LotItemService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
