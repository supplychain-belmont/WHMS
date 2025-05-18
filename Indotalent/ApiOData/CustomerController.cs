using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Customers;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class CustomerController : ODataController
    {
        private readonly CustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(CustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<CustomerDto> Get()
        {
            return _customerService
                .GetAll()
                .Include(x => x.CustomerGroup)
                .Include(x => x.CustomerCategory)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<CustomerDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = await _customerService.GetByRowGuidAsync(key,
                    x => x.CustomerGroup, x => x.CustomerCategory)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(customer);
        }

        public async Task<ActionResult<CustomerDto>> Post([FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);
            await _customerService.AddAsync(customer);
            return Created();
        }

        public async Task<ActionResult<CustomerDto>> Put([FromRoute] Guid key, [FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomer = await _customerService.GetByRowGuidAsync(key);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            if (currentCustomer.Number != customerDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(customerDto, currentCustomer);
            await _customerService.UpdateAsync(currentCustomer);
            return NoContent();
        }

        public async Task<ActionResult<CustomerDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<CustomerDto> customerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomer = await _customerService.GetByRowGuidAsync(key);
            if (currentCustomer == null)
            {
                return NotFound();
            }

            customerDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && currentCustomer.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<CustomerDto>(currentCustomer);
            customerDto.Patch(dto);

            var entity = _mapper.Map(dto, currentCustomer);

            await _customerService.UpdateAsync(entity);
            return Updated(_mapper.Map<CustomerDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customerService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
