using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;

namespace Indotalent.Applications.Products
{
    public class ProductService : Repository<Product>
    {
        private readonly NumberSequenceService _numberSequenceService;

        public ProductService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
        }

        public override Task AddAsync(Product? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(Product), "", "ART");
            return base.AddAsync(entity);
        }
    }
}
