using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.VendorGroups;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class VendorGroupController : ODataController
    {
        private readonly VendorGroupService _vendorGroupService;
        private readonly IMapper _mapper;

        public VendorGroupController(VendorGroupService vendorGroupService, IMapper mapper)
        {
            _vendorGroupService = vendorGroupService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<VendorGroupDto> Get()
        {
            return _vendorGroupService
                .GetAll()
                .ProjectTo<VendorGroupDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<VendorGroupDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorGroup = await _vendorGroupService.GetByRowGuidAsync(key);
            return Ok(_mapper.Map<VendorGroupDto>(vendorGroup));
        }

        public async Task<ActionResult<VendorGroupDto>> Post([FromBody] VendorGroupDto vendorGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorGroup = _mapper.Map<VendorGroup>(vendorGroupDto);
            await _vendorGroupService.AddAsync(vendorGroup);
            return Created();
        }

        public async Task<ActionResult<VendorGroupDto>> Put([FromRoute] Guid key,
            [FromBody] VendorGroupDto vendorGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendorGroup = await _vendorGroupService.GetByRowGuidAsync(key);
            if (currentVendorGroup == null)
            {
                return NotFound();
            }

            _mapper.Map(vendorGroupDto, currentVendorGroup);
            await _vendorGroupService.UpdateAsync(currentVendorGroup);
            return NoContent();
        }

        public async Task<ActionResult<VendorGroupDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<VendorGroupDto> vendorGroupDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendorGroup = await _vendorGroupService.GetByRowGuidAsync(key);
            if (currentVendorGroup == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<VendorGroupDto>(currentVendorGroup);
            vendorGroupDto.Patch(dto);

            var entity = _mapper.Map(dto, currentVendorGroup);

            await _vendorGroupService.UpdateAsync(entity);
            return Updated(_mapper.Map<VendorGroupDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorGroupService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
