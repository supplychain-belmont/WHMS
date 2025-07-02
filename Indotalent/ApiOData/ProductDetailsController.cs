using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class ProductDetailsController : ODataController
    {
        private readonly ProductDetailsService _productDetailsService;
        private readonly IMapper _mapper;

        public ProductDetailsController(ProductDetailsService productDetailsService, IMapper mapper)
        {
            _productDetailsService = productDetailsService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ProductDetailsDto> Get()
        {
            return _productDetailsService
                .GetAll()
                .Include(pd => pd.Product)
                .Include(pd => pd.NationalProductOrder)
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<ProductDetailsDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productDetails = await _productDetailsService.GetByRowGuidAsync(key,
                    pd => pd.Product,
                    pd => pd.NationalProductOrder)
                .ProjectTo<ProductDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (productDetails == null)
            {
                return NotFound();
            }

            return Ok(productDetails);
        }

        public async Task<ActionResult<ProductDetailsDto>> Post([FromBody] ProductDetailsDto productDetailsDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productDetails = _mapper.Map<ProductDetails>(productDetailsDto);
            await _productDetailsService.AddAsync(productDetails);

            var resultDto = _mapper.Map<ProductDetailsDto>(productDetails);
            return Created(resultDto);
        }

        public async Task<ActionResult> Put([FromRoute] Guid key, [FromBody] ProductDetailsDto productDetailsDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentProductDetails = await _productDetailsService.GetByRowGuidAsync(key);
            if (currentProductDetails == null)
            {
                return NotFound();
            }

            _mapper.Map(productDetailsDto, currentProductDetails);
            await _productDetailsService.UpdateAsync(currentProductDetails);

            return NoContent();
        }

        public async Task<ActionResult<ProductDetailsDto>> Patch([FromRoute] Guid key, [FromBody] Delta<ProductDetailsDto> productDetailsDelta)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentProductDetails = await _productDetailsService.GetByRowGuidAsync(key);
            if (currentProductDetails == null)
            {
                return NotFound();
            }

            var productDetailsDto = _mapper.Map<ProductDetailsDto>(currentProductDetails);
            productDetailsDelta.Patch(productDetailsDto);

            _mapper.Map(productDetailsDto, currentProductDetails);
            await _productDetailsService.UpdateAsync(currentProductDetails);

            var resultDto = _mapper.Map<ProductDetailsDto>(currentProductDetails);
            return Updated(resultDto);
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _productDetailsService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
