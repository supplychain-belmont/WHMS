using Indotalent.Domain.Grid;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

namespace Indotalent.Templates.Service;

public class GridService : Repository<Grid>
{
    public GridService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) : base(context, httpContextAccessor, auditColumnTransformer)
    {
    }
}
