using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.VendorCategories;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class VendorCategoryController : ODataController
    {
        private readonly VendorCategoryService _vendorCategoryService;
        private readonly IMapper _mapper;

        public VendorCategoryController(VendorCategoryService vendorCategoryService, IMapper mapper)
        {
            _vendorCategoryService = vendorCategoryService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<VendorCategoryDto> Get()
        {
            return _vendorCategoryService
                .GetAll()
                .ProjectTo<VendorCategoryDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<VendorCategoryDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCategory = await _vendorCategoryService.GetByRowGuidAsync(key);
            return Ok(_mapper.Map<VendorCategoryDto>(vendorCategory));
        }

        public async Task<ActionResult<VendorCategoryDto>> Post([FromBody] VendorCategoryDto vendorCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorCategory = _mapper.Map<VendorCategory>(vendorCategoryDto);
            await _vendorCategoryService.AddAsync(vendorCategory);
            return Created();
        }

        public async Task<ActionResult<VendorCategoryDto>> Put([FromRoute] Guid key,
            [FromBody] VendorCategoryDto vendorCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendor = await _vendorCategoryService.GetByRowGuidAsync(key);
            if (currentVendor == null)
            {
                return NotFound();
            }

            _mapper.Map(vendorCategoryDto, currentVendor);
            await _vendorCategoryService.UpdateAsync(currentVendor);
            return NoContent();
        }

        public async Task<ActionResult<VendorCategoryDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<VendorCategoryDto> vendorCategoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendor = await _vendorCategoryService.GetByRowGuidAsync(key);
            if (currentVendor == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<VendorCategoryDto>(currentVendor);
            vendorCategoryDto.Patch(dto);

            var entity = _mapper.Map(dto, currentVendor);

            await _vendorCategoryService.UpdateAsync(entity);
            return Updated(_mapper.Map<VendorCategoryDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorCategoryService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
