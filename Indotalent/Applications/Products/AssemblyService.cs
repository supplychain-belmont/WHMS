using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Products;

public class AssemblyService : Repository<Assembly>
{
    private readonly ProductService _productService;

    public AssemblyService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer,
        ProductService productService) :
        base(context, httpContextAccessor, auditColumnTransformer)
    {
        _productService = productService;
    }
}
