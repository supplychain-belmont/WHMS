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

        var productGroupId = await productGroupService.GetAll().Where(x => x.Name == "Exterior").FirstOrDefaultAsync();
        var unitMeasureId = await unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefaultAsync();

        await productService.AddAsync(new Product
        {
            Name = "Jhoston",
            Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
            ProductGroupId = productGroupId!.Id,
            UnitMeasureId = unitMeasureId!.Id,
            Physical = true,
            IsAssembly = true,
            UnitPrice = 2132m,
            M3 = 3m,
        });
        await productService.AddAsync(new Product
        {
            Name = "Kimone",
            Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
            ProductGroupId = productGroupId.Id,
            UnitMeasureId = unitMeasureId.Id,
            Physical = true,
            IsAssembly = true,
            UnitPrice = 1150.0m,
            M3 = 2.1m,
        });

        var assemblyProducts = await productService.GetAll()
            .Where(p => p.IsAssembly)
            .ToListAsync();

        Random random = new();

        foreach (Product p in assemblyProducts)
        {
            var child = await productService.GetAll()
                .Where(pr => pr.Name.StartsWith(p.Name))
                .Where(pr => !pr.IsAssembly)
                .ToListAsync();

            foreach (Product product in child)
            {
                var quantity = random.Next(1, 5);
                if (product.Name.Contains("mesa"))
                {
                    quantity = 1;
                }

                await assemblyProductService.AddAsync(new AssemblyProduct
                {
                    ProductId = product.Id, Quantity = quantity, AssemblyId = p.Id,
                });
            }
        }
    }
}
