using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

namespace Indotalent.Applications.CustomerCategories
{
    public class CustomerCategoryService : Repository<CustomerCategory>
    {
        public CustomerCategoryService(
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
