using Indotalent.Data;
using Indotalent.Domain.Contracts;

namespace Indotalent.Infrastructures.Repositories
{
    public interface IAuditColumnTransformer
    {
        Task TransformAsync(IHasAudit entity, ApplicationDbContext context);
    }
}
