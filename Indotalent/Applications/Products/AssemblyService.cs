using Indotalent.Application.Products;
using Indotalent.Applications.InventoryTransactions;
using Indotalent.Data;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Applications.Products;

public class AssemblyService : Repository<Assembly>
{
    private readonly ProductService _productService;
    private readonly AssemblyChildService _assemblyChildService;
    private readonly AssemblyProcessor _assemblyProcessor;
    private readonly InventoryStockService _inventoryStockService;
    private readonly InventoryTransactionService _inventoryTransactionService;

    public AssemblyService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer,
        ProductService productService,
        AssemblyChildService assemblyChildService,
        AssemblyProcessor assemblyProcessor,
        InventoryStockService inventoryStockService,
        InventoryTransactionService inventoryTransactionService) :
        base(context, httpContextAccessor, auditColumnTransformer)
    {
        _productService = productService;
        _assemblyChildService = assemblyChildService;
        _assemblyProcessor = assemblyProcessor;
        _inventoryStockService = inventoryStockService;
        _inventoryTransactionService = inventoryTransactionService;
    }

    public async Task CreateAssemblyAsync(int productId, int warehouseId, int quantity = 1)
    {
        var assembly = await GetAll()
            .Where(x => x.ProductId == productId)
            .Include(x => x.Product)
            .Where(x => x.Product!.IsAssembly)
            .FirstOrDefaultAsync();
        if (assembly == null)
        {
            throw new ArgumentException("Assembly not found.");
        }

        var children = await _assemblyChildService
            .GetAll()
            .Where(x => x.AssemblyId == assembly.Id)
            .OrderBy(x => x.Quantity)
            .Include(x => x.Product)
            .ToListAsync();

        var stockInfo = await _inventoryStockService
            .GetAll()
            .Where(x => x.WarehouseId == warehouseId &&
                        children.Select(ac => ac.ProductId).Contains(x.ProductId!.Value))
            .Select(it =>
                new
                {
                    it.ProductId,
                    it.Product,
                    it.WarehouseId,
                    it.Stock,
                    it.Warehouse
                })
            .ToListAsync();

        foreach (AssemblyChild assemblyChild in children)
        {
            var stock = stockInfo.FirstOrDefault(x => x.ProductId == assemblyChild.ProductId);
            if (stock == null) continue;

            if (stock.Stock < assemblyChild.Quantity * quantity)
            {
                throw new InvalidOperationException(
                    $"Insufficient stock for product '{assemblyChild.Product!.Name}' in warehouse '{stock.Warehouse}'.");
            }
        }

        var transactions =
            _assemblyProcessor.CreateInventoryTransactions(assembly, children, warehouseId, quantity);

        var transaction = _assemblyProcessor.CreateAssemblyTransaction(assembly, warehouseId, quantity);
        transactions.Add(transaction);
        await _inventoryTransactionService.AddRangeAsync(transactions);
    }
}
