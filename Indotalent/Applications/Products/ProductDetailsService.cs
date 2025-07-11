using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Applications.Products
{
    public class ProductDetailsService : Repository<ProductDetails>
    {
        public ProductDetailsService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer) :
            base(context, httpContextAccessor, auditColumnTransformer)
        {
        }
    }
}
