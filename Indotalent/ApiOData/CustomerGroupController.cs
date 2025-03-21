using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.CustomerGroups;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class CustomerGroupController : ODataController
    {
        private readonly CustomerGroupService _customerGroupService;
        private readonly IMapper _mapper;

        public CustomerGroupController(CustomerGroupService customerGroupService, IMapper mapper)
        {
            _customerGroupService = customerGroupService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<CustomerGroupDto> Get()
        {
            return _customerGroupService
                .GetAll()
                .ProjectTo<CustomerGroupDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<CustomerGroupDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = await _customerGroupService.GetByRowGuidAsync(key);
            return Ok(_mapper.Map<CustomerGroupDto>(customer));
        }

        public async Task<ActionResult<CustomerGroupDto>> Post([FromBody] CustomerGroupDto customerGroupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<CustomerGroup>(customerGroupDto);
            await _customerGroupService.AddAsync(customer);
            return Created();
        }

        public async Task<ActionResult<CustomerGroupDto>> Put([FromRoute] Guid key,
            [FromBody] CustomerGroupDto customerGroupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomerGroup = await _customerGroupService.GetByRowGuidAsync(key);
            if (currentCustomerGroup == null)
            {
                return NotFound();
            }

            _mapper.Map(customerGroupDto, currentCustomerGroup);
            await _customerGroupService.UpdateAsync(currentCustomerGroup);
            return NoContent();
        }

        public async Task<ActionResult<CustomerGroupDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<CustomerGroupDto> customerGroupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomerGroup = await _customerGroupService.GetByRowGuidAsync(key);
            if (currentCustomerGroup == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<CustomerGroupDto>(currentCustomerGroup);
            customerGroupDto.Patch(dto);

            var entity = _mapper.Map(dto, currentCustomerGroup);

            await _customerGroupService.UpdateAsync(entity);
            return Updated(_mapper.Map<CustomerGroupDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customerGroupService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
