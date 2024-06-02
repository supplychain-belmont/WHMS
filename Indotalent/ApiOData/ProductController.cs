using Indotalent.Applications.Products;
using Indotalent.DTOs;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Indotalent.ApiOData
{
    public class ProductController : ODataController
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [EnableQuery]
        public IQueryable<ProductDto> Get()
        {
            return _productService
                .GetAll()
                .Include(x => x.ProductGroup)
                .Include(x => x.UnitMeasure)
                .Select(rec => new ProductDto
                {
                    Id = rec.Id,
                    Name = rec.Name,
                    Number = rec.Number,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                    ProductGroup = rec.ProductGroup != null ? rec.ProductGroup.Name : null,
                    UnitMeasure = rec.UnitMeasure != null ? rec.UnitMeasure.Name : null,
                    UnitPrice = rec.UnitPrice,
                    Physical = rec.Physical,
                });
        }
    }
}