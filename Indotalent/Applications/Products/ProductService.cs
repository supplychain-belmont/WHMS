using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Domain.Entities;

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

        public override async Task AddAsync(Product? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(Product), "", "ART");
            entity.CalculateUnitPrice();
            await base.AddAsync(entity);
        }

        public override async Task UpdateAsync(Product? entity)
        {
            entity!.CalculateUnitPrice();
            await base.UpdateAsync(entity);
        }
    }
}
