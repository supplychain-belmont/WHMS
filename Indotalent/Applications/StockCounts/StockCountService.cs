using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

namespace Indotalent.Applications.StockCounts
{
    public class StockCountService : Repository<StockCount>
    {
        public StockCountService(
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
