using Indotalent.Application.Products;
using Indotalent.Applications.NumberSequences;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Applications.Products
{
    public class ProductService : Repository<Product>
    {
        private readonly NumberSequenceService _numberSequenceService;
        private readonly ProductProcessor _productProcessor;

        public ProductService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer,
            NumberSequenceService numberSequenceService,
            ProductProcessor productProcessor) :
            base(
                context,
                httpContextAccessor,
                auditColumnTransformer)
        {
            _numberSequenceService = numberSequenceService;
            _productProcessor = productProcessor;
        }

        public override async Task AddAsync(Product? entity)
        {
            entity!.Number = _numberSequenceService.GenerateNumber(nameof(Product), "", "ART");
            _productProcessor.CalculateUnitPrice(entity);
            await base.AddAsync(entity);
        }

        public override async Task UpdateAsync(Product? entity)
        {
            _productProcessor.CalculateUnitPrice(entity!);
            await base.UpdateAsync(entity);
        }
    }
}
