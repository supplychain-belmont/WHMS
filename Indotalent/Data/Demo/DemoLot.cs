using Indotalent.Applications.Lots;
using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.Products;
using Indotalent.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Data.Demo;

public class DemoLot
{
    public static async Task GenerateAsync(IServiceProvider services)
    {
        var lotService = services.GetRequiredService<LotService>();
        var lotItemService = services.GetRequiredService<LotItemService>();
        var productService = services.GetRequiredService<ProductService>();
        var numberSequenceService = services.GetRequiredService<NumberSequenceService>();

        await lotService.AddAsync(new Lot
        {
            Number = numberSequenceService.GenerateNumber(nameof(Lot), "", "LOT"),
            Name = "Jhoston",
            ContainerM3 = 93.5m,
            TotalTransportContainerCost = 12000m,
            TotalAgencyCost = 1550m,
        });

        await lotService.AddAsync(new Lot
        {
            Number = numberSequenceService.GenerateNumber(nameof(Lot), "", "LOT"),
            Name = "Kimone",
            ContainerM3 = 100.0m,
            TotalTransportContainerCost = 15000m,
            TotalAgencyCost = 2000m,
        });

        var lots = await lotService
            .GetAll()
            .ToListAsync();
        foreach (Lot lot in lots)
        {
            var child = await productService.GetAll()
                .Where(pr => pr.Name.StartsWith(lot.Name))
                .ToListAsync();

            foreach (Product product in child)
            {
                var quantity = new Random().Next(1, 8);
                if (product.Name.Contains("mesa"))
                {
                    quantity = 1;
                }

                await lotItemService.AddAsync(new LotItem
                {
                    LotId = lot.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    UnitCost = product.UnitCost!.Value,
                    UnitCostBrazil = product.UnitCost!.Value,
                    UnitCostDiscounted = product.UnitCost!.Value * 0.9m, // Assuming a 10% discount
                });
            }
        }

        await lotService.AddAsync(new Lot
        {
            Number = numberSequenceService.GenerateNumber(nameof(Lot), "", "LOT"),
            Name = "Lot 3",
            ContainerM3 = 100.0m,
            TotalTransportContainerCost = 15000m,
            TotalAgencyCost = 2000m,
        });

        var lastLot = await lotService
            .GetAll()
            .OrderByDescending(l => l.Id)
            .FirstOrDefaultAsync();

        var products = await productService.GetAll()
            .Take(new Random().Next(1, 10))
            .ToListAsync();

        foreach (Product product in products)
        {
            var quantity = new Random().Next(1, 8);
            if (product.Name.Contains("mesa"))
            {
                quantity = 1;
            }

            await lotItemService.AddAsync(new LotItem
            {
                LotId = lastLot!.Id,
                ProductId = product.Id,
                Quantity = quantity,
                UnitCost = product.UnitCost!.Value,
                UnitCostBrazil = product.UnitCost!.Value,
                UnitCostDiscounted = product.UnitCost!.Value * 0.9m, // Assuming a 10% discount
            });
        }
    }
}
