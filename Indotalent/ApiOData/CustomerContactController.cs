using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.CustomerContacts;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class CustomerContactController : ODataController
    {
        private readonly CustomerContactService _customerContactService;
        private readonly IMapper _mapper;

        public CustomerContactController(CustomerContactService customerContactService, IMapper mapper)
        {
            _customerContactService = customerContactService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<CustomerContactDto> Get()
        {
            return _customerContactService
                .GetAll()
                .Include(x => x.Customer)
                .ProjectTo<CustomerContactDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<CustomerContactDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = await _customerContactService.GetByRowGuidAsync(key,
                    x => x.Customer)
                .ProjectTo<CustomerContactDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(customer);
        }

        public async Task<ActionResult<CustomerContactDto>> Post([FromBody] CustomerContactDto customerContactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<CustomerContact>(customerContactDto);
            await _customerContactService.AddAsync(customer);
            return Created();
        }

        public async Task<ActionResult<CustomerContactDto>> Put([FromRoute] Guid key,
            [FromBody] CustomerContactDto customerContactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomerContact = await _customerContactService.GetByRowGuidAsync(key);
            if (currentCustomerContact == null)
            {
                return NotFound();
            }

            if (currentCustomerContact.Number != customerContactDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(customerContactDto, currentCustomerContact);
            await _customerContactService.UpdateAsync(currentCustomerContact);
            return NoContent();
        }

        public async Task<ActionResult<CustomerContactDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<CustomerContactDto> customerContactDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomerContact = await _customerContactService.GetByRowGuidAsync(key);
            if (currentCustomerContact == null)
            {
                return NotFound();
            }

            customerContactDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && currentCustomerContact.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<CustomerContactDto>(currentCustomerContact);
            customerContactDto.Patch(dto);

            var entity = _mapper.Map(dto, currentCustomerContact);

            await _customerContactService.UpdateAsync(entity);
            return Updated(_mapper.Map<CustomerContactDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customerContactService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
