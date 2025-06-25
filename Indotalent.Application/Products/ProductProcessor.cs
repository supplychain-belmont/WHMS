using Indotalent.Domain.Entities;

namespace Indotalent.Application.Products;

public class ProductProcessor
{
    public void CalculateUnitPrice(Product entity)
    {
        entity.UnitPrice40 = entity.UnitCost * 1.4m;
        entity.UnitPrice50 = entity.UnitCost * 1.5m;
        entity.UnitPrice60 = entity.UnitCost * 1.6m;
        entity.UnitPrice = entity.UnitPrice60!.Value;
    }

    public Product Clone(Product entity)
    {
        return new Product
        {
            Name = entity.Name,
            Number = entity.Number,
            Description = entity.Description,
            UnitPrice = entity.UnitPrice,
            UnitCost = entity.UnitCost,
            UnitPrice40 = entity.UnitPrice40,
            UnitPrice50 = entity.UnitPrice50,
            UnitPrice60 = entity.UnitPrice60,
            Physical = entity.Physical,
            IsAssembly = entity.IsAssembly,
            AssemblyId = entity.AssemblyId,
            UnitMeasureId = entity.UnitMeasureId,
            ProductGroupId = entity.ProductGroupId,
            M3 = entity.M3,
            Color = entity.Color,
            ColorCode = entity.ColorCode,
            Material = entity.Material,
            TapestryCode = entity.TapestryCode,
            IsNationalProduct = entity.IsNationalProduct,
            ProductCategory = entity.ProductCategory,
        };
    }
}
