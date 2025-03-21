using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

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
