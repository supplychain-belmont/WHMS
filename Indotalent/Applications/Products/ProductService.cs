using Indotalent.Application.Products;
using Indotalent.Applications.NumberSequences;
using Indotalent.Domain.Entities;
using Indotalent.Persistence;
using Indotalent.Persistence.Repositories;

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
            entity.UnitCost ??= 0m;
            _productProcessor.CalculateUnitPrice(entity);
            await base.AddAsync(entity);

            if (entity.IsAssembly)
            {
                var assembly = new Assembly { ProductId = entity.Id, Description = $"Assembly for {entity.Name}" };
                await _context.Set<Assembly>().AddAsync(assembly);
                await _context.SaveChangesAsync();
            }
        }

        public override async Task UpdateAsync(Product? entity)
        {
            _productProcessor.CalculateUnitPrice(entity!);
            await base.UpdateAsync(entity);
        }
    }
}
