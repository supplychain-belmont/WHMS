using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Http;

namespace Indotalent.Applications.NationalProductOrders
{
    public class NationalProductOrderService : Repository<NationalProductOrder>
    {
        public NationalProductOrderService(
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
