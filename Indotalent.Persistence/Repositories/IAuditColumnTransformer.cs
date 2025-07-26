using Indotalent.Domain.Contracts;

namespace Indotalent.Persistence.Repositories
{
    public interface IAuditColumnTransformer
    {
        Task TransformAsync(IHasAudit entity, ApplicationDbContext context);
    }
}
