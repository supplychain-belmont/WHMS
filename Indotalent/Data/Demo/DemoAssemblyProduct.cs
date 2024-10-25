using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.ProductGroups;
using Indotalent.Applications.Products;
using Indotalent.Applications.UnitMeasures;
using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Data.Demo;

public static class DemoAssemblyProduct
{
    public static async Task GenerateAsync(IServiceProvider services)
    {
        var productService = services.GetRequiredService<ProductService>();
        var assemblyProductService = services.GetRequiredService<AssemblyProductService>();
        var numberSequenceService = services.GetRequiredService<NumberSequenceService>();
        var productGroupService = services.GetRequiredService<ProductGroupService>();
        var unitMeasureService = services.GetRequiredService<UnitMeasureService>();

        var productGroupId = await productGroupService.GetAll().Where(x => x.Name == "Hardware").FirstOrDefaultAsync();
        var unitMeasureId = await unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefaultAsync();

        await productService.AddAsync(new Product
        {
            Name = "Assembly Product 1",
            Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
            ProductGroupId = productGroupId!.Id,
            UnitMeasureId = unitMeasureId!.Id,
            Physical = true,
            IsAssembly = true,
            UnitPrice = 5000.0m,
            M3 = 1.2m,
        });
        await productService.AddAsync(new Product
        {
            Name = "Assembly Product 2",
            Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
            ProductGroupId = productGroupId.Id,
            UnitMeasureId = unitMeasureId.Id,
            Physical = true,
            IsAssembly = true,
            UnitPrice = 2000.0m,
            M3 = 1.7m,
        });

        var assemblyProducts = await productService.GetAll()
            .Where(p => p.IsAssembly)
            .ToListAsync();

        var products = await productService.GetAll()
            .Where(p => !p.IsAssembly)
            .ToListAsync();

        Random random = new();

        foreach (Product p in assemblyProducts)
        {
            for (int i = 0; i < random.Next(2, 3); i++)
            {
                var randomProduct = products[random.Next(products.Count)];
                await assemblyProductService.AddAsync(new AssemblyProduct
                {
                    ProductId = randomProduct.Id,
                    Quantity = random.Next(1, 10),
                    AssemblyId = p.Id,
                });
            }
        }
    }
}
