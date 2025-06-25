using Indotalent.Application.Products;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Products;

public class AssemblyChildService : Repository<AssemblyChild>
{
    private readonly ProductProcessor _productProcessor;

    public AssemblyChildService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer, ProductProcessor productProcessor) : base(context,
        httpContextAccessor, auditColumnTransformer)
    {
        _productProcessor = productProcessor;
    }

    public override async Task AddAsync(AssemblyChild? entity)
    {
        await base.AddAsync(entity);

        var assembly = await _context.Set<Assembly>()
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == entity!.AssemblyId);
        var master = await _context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == assembly!.ProductId);
        var child = await _context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == entity!.ProductId);

        if (master == null || child == null)
        {
            throw new ArgumentException("Assembly or child product not found.");
        }

        master.UnitCost += (child!.UnitCost * entity!.Quantity);

        _productProcessor.CalculateUnitPrice(master);
        _context.Set<Product>().Update(master);
        await _context.SaveChangesAsync();
    }

    public override async Task DeleteByIdAsync(int? id)
    {
        var entity = await GetByIdAsync(id);
        var assembly = await _context.Set<Assembly>()
            .Include(x => x.Product)
            .FirstOrDefaultAsync(x => x.Id == entity!.AssemblyId);
        var master = await _context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == assembly!.ProductId);
        var child = await _context.Set<Product>()
            .FirstOrDefaultAsync(x => x.Id == entity!.ProductId);

        if (master == null || child == null)
        {
            throw new ArgumentException("Assembly or child product not found.");
        }

        master.UnitCost -= (child!.UnitCost * entity!.Quantity);

        _productProcessor.CalculateUnitPrice(master);
        _context.Set<Product>().Update(master);
        await _context.SaveChangesAsync();
        await base.DeleteByIdAsync(id);
    }
}
