using System.Linq;

using AutoMapper;

using Indotalent.Applications.Products;
using Indotalent.Applications.Products;
using Indotalent.DTOs;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class ProductController : ODataController
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
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
        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<ProductDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productService.GetByIdAsync(key, x => x.ProductGroup, x => x.UnitMeasure);
            if (product == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ProductDto>(product);
            return Ok(dto);
        }
        public async Task<ActionResult<ProductDto>> Post([FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _mapper.Map<Product>(productDto);
            await _productService.AddAsync(product);
            var createdDto = _mapper.Map<ProductDto>(product);

            return Created(createdDto);
        }
        public async Task<ActionResult<ProductDto>> Put([FromRoute] int key, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentProduct = await _productService.GetByIdAsync(key);
            if (currentProduct == null)
            {
                return NotFound();
            }

            if (currentProduct.Number != productDto.Number)
            {
                return BadRequest("Unable to update product number.");
            }

            _mapper.Map(productDto, currentProduct);
            await _productService.UpdateAsync(currentProduct);
            return NoContent();
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
