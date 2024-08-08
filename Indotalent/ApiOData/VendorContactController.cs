using AutoMapper;

using Indotalent.Applications.VendorContacts;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class VendorContactController : ODataController
    {
        private readonly VendorContactService _vendorContactService;
        private readonly IMapper _mapper;

        public VendorContactController(VendorContactService vendorContactService, IMapper mapper)
        {
            _vendorContactService = vendorContactService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<VendorContactDto> Get()
        {
            return _vendorContactService
                .GetAll()
                .Include(x => x.Vendor)
                .Select(rec => _mapper.Map<VendorContactDto>(rec));
        }

        public async Task<ActionResult<VendorContactDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _vendorContactService.GetByRowGuidAsync(key,
                x => x.Vendor);
            return Ok(_mapper.Map<VendorContactDto>(vendor));
        }

        public async Task<ActionResult<VendorContactDto>> Post([FromBody] VendorContactDto vendorContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendorContact = _mapper.Map<VendorContact>(vendorContactDto);
            await _vendorContactService.AddAsync(vendorContact);
            return Created();
        }

        public async Task<ActionResult<VendorContactDto>> Put([FromRoute] Guid key,
            [FromBody] VendorContactDto vendorContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendorContact = await _vendorContactService.GetByRowGuidAsync(key);
            if (currentVendorContact == null)
            {
                return NotFound();
            }

            if (currentVendorContact.Number != vendorContactDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(vendorContactDto, currentVendorContact);
            await _vendorContactService.UpdateAsync(currentVendorContact);
            return NoContent();
        }

        public async Task<ActionResult<VendorContactDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<VendorContactDto> vendorContactDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendorContact = await _vendorContactService.GetByRowGuidAsync(key);
            if (currentVendorContact == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<VendorContactDto>(currentVendorContact);
            vendorContactDto.Patch(dto);

            var entity = _mapper.Map(dto, currentVendorContact);

            await _vendorContactService.UpdateAsync(entity);
            return Updated(_mapper.Map<VendorContactDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vendorContactService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
