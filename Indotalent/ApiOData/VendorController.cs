using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Vendors;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class VendorController : ODataController
    {
        private readonly VendorService _vendorService;
        private readonly IMapper _mapper;

        public VendorController(VendorService vendorService, IMapper mapper)
        {
            _vendorService = vendorService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<VendorDto> Get()
        {
            return _vendorService
                .GetAll()
                .Include(x => x.VendorGroup)
                .Include(x => x.VendorCategory)
                .ProjectTo<VendorDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<VendorDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _vendorService.GetByRowGuidAsync(key,
                    x => x.VendorGroup, x => x.VendorCategory)
                .ProjectTo<VendorDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(vendor);
        }

        public async Task<ActionResult<VendorDto>> Post([FromBody] VendorDto vendorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = _mapper.Map<Vendor>(vendorDto);
            await _vendorService.AddAsync(vendor);
            return Created();
        }

        public async Task<ActionResult<VendorDto>> Put([FromRoute] Guid key, [FromBody] VendorDto vendorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendor = await _vendorService.GetByRowGuidAsync(key);
            if (currentVendor == null)
            {
                return NotFound();
            }

            if (currentVendor.Number != vendorDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(vendorDto, currentVendor);
            await _vendorService.UpdateAsync(currentVendor);
            return NoContent();
        }

        public async Task<ActionResult<VendorDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<VendorDto> vendorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendor = await _vendorService.GetByRowGuidAsync(key);
            if (currentVendor == null)
            {
                return NotFound();
            }

            vendorDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && currentVendor.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<VendorDto>(currentVendor);
            vendorDto.Patch(dto);

            var entity = _mapper.Map(dto, currentVendor);

            await _vendorService.UpdateAsync(entity);
            return Updated(_mapper.Map<VendorDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
