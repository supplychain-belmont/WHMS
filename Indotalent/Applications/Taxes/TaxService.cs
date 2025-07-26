using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

namespace Indotalent.Applications.Taxes
{
    public class TaxService : Repository<Tax>
    {
        public TaxService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer) :
                base(
                    context,
                    httpContextAccessor,
                    auditColumnTransformer)
        {
        }


    }
}
