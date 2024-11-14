using Indotalent.Data;
using Indotalent.Infrastructures.Repositories;
using Indotalent.Models.Entities;

namespace Indotalent.Applications.Products;

public class AssemblyProductService : Repository<AssemblyProduct>
{
    private readonly ProductService _productService;

    public AssemblyProductService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer,
        ProductService productService) :
        base(context, httpContextAccessor, auditColumnTransformer)
    {
        _productService = productService;
    }

    public override async Task AddAsync(AssemblyProduct? entity)
    {
        await base.AddAsync(entity);
        var product = await _productService.GetByIdAsync(entity?.ProductId);
        if (product != null)
        {
            product.AssemblyId = entity!.AssemblyId;
        }
        await _productService.UpdateAsync(product);
    }
}