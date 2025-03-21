using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

namespace Indotalent.Applications.ProductGroups
{
    public class ProductGroupService : Repository<ProductGroup>
    {
        public ProductGroupService(
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
