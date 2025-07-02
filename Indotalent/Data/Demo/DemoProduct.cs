using Indotalent.Applications.NumberSequences;
using Indotalent.Applications.ProductGroups;
using Indotalent.Applications.Products;
using Indotalent.Applications.UnitMeasures;
using Indotalent.Domain.Entities;

namespace Indotalent.Data.Demo
{
    public static class DemoProduct
    {
        public static async Task GenerateAsync(IServiceProvider services)
        {
            var productService = services.GetRequiredService<ProductService>();
            var numberSequenceService = services.GetRequiredService<NumberSequenceService>();
            var productGroupService = services.GetRequiredService<ProductGroupService>();
            var unitMeasureService = services.GetRequiredService<UnitMeasureService>();

            var groups = productGroupService.GetAll().Select(x => x.Id).ToArray();
            var measures = unitMeasureService.GetAll().Select(x => x.Id).ToArray();
            var prices = new double[] { 500.0, 1000.0, 2000.0, 3000.0, 4000.0, 5000.0 };

            Random random = new Random();

            await productService.AddAsync(new Product
            {
                Name = "Asiento con 1BR",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 250.0m,
                UnitPrice = 0m,
                M3 = 0.65m
            });
            await productService.AddAsync(new Product
            {
                Name = "Canto Curvo",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 500.0m,
                UnitPrice = 0m,
                M3 = 1.2m
            });
            await productService.AddAsync(new Product
            {
                Name = "Puff curvo",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 150.0m,
                UnitPrice = 0m,
                M3 = 0.26m
            });
            await productService.AddAsync(new Product
            {
                Name = "Casablanca",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Salón").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 860m,
                UnitPrice = 0m,
                M3 = 2.2m
            });
            await productService.AddAsync(new Product
            {
                Name = "Tensas",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 1120m,
                UnitPrice = 0m,
                M3 = 1.5m
            });
            await productService.AddAsync(new Product
            {
                Name = "Taracra",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 629m,
                UnitPrice = 0m,
                M3 = 1.1m
            });
            await productService.AddAsync(new Product
            {
                Name = "Vittoria",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 340m,
                UnitPrice = 0m,
                M3 = 1.2m
            });
            await productService.AddAsync(new Product
            {
                Name = "Delluca",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Salón").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 110,
                UnitPrice = 0m,
                M3 = 1.8m
            });
            await productService.AddAsync(new Product
            {
                Name = "Marsalla",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Salón").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 2500m,
                UnitPrice = 0m,
                M3 = 2.32m
            });
            await productService.AddAsync(new Product
            {
                Name = "Zerfield",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 496m,
                UnitPrice = 0m,
                M3 = 0.8m
            });
            await productService.AddAsync(new Product
            {
                Name = "Jhoston mesa",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 1520m,
                UnitPrice = 0m,
                M3 = 2.5m
            });
            await productService.AddAsync(new Product
            {
                Name = "Jhoston silla",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 610m,
                UnitPrice = 0m,
                M3 = 0.5m
            });
            await productService.AddAsync(new Product
            {
                Name = "Kimone mesa",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 910m,
                UnitPrice = 0m,
                M3 = 1.5m
            });
            await productService.AddAsync(new Product
            {
                Name = "Kimone silla",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Comedor").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 250m,
                UnitPrice = 0m,
                M3 = 0.65m
            });
            await productService.AddAsync(new Product
            {
                Name = "Watson",
                Number = numberSequenceService.GenerateNumber(nameof(Product), "", "ART"),
                ProductGroupId = productGroupService.GetAll().Where(x => x.Name == "Oficina").FirstOrDefault()!.Id,
                UnitMeasureId = unitMeasureService.GetAll().Where(x => x.Name == "u").FirstOrDefault()!.Id,
                Physical = true,
                UnitCost = 238m,
                UnitPrice = 0m,
                M3 = 0.5m
            });
        }
    }
}
