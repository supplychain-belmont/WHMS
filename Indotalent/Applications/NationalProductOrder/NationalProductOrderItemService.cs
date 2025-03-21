using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

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
