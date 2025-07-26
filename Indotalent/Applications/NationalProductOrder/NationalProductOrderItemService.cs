using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

namespace Indotalent.Applications.NationalProductOrderItems
{
    public class NationalProductOrderItemService : Repository<NationalProductOrderItem>
    {
        public NationalProductOrderItemService(
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
