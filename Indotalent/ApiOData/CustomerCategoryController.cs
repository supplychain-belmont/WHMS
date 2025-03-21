using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.CustomerCategories;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class CustomerCategoryController : ODataController
    {
        private readonly IMapper _mapper;
        private readonly CustomerCategoryService _customerCategoryService;

        public CustomerCategoryController(CustomerCategoryService customerCategoryService, IMapper mapper)
        {
            _mapper = mapper;
            _customerCategoryService = customerCategoryService;
        }

        [EnableQuery]
        public IQueryable<CustomerCategoryDto> Get()
        {
            return _customerCategoryService
                .GetAll()
                .ProjectTo<CustomerCategoryDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<CustomerCategoryDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = await _customerCategoryService.GetByRowGuidAsync(key);
            return Ok(_mapper.Map<CustomerCategoryDto>(customer));
        }

        public async Task<ActionResult<CustomerCategoryDto>> Post([FromBody] CustomerCategoryDto customerCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customer = _mapper.Map<CustomerCategory>(customerCategoryDto);
            await _customerCategoryService.AddAsync(customer);
            return Created();
        }

        public async Task<ActionResult<CustomerCategoryDto>> Put([FromRoute] Guid key,
            [FromBody] CustomerCategoryDto customerCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var customerCategory = await _customerCategoryService.GetByRowGuidAsync(key);
            if (customerCategory == null)
            {
                return NotFound();
            }

            _mapper.Map(customerCategoryDto, customerCategory);
            await _customerCategoryService.UpdateAsync(customerCategory);
            return NoContent();
        }

        public async Task<ActionResult<CustomerCategoryDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<CustomerCategoryDto> customerCategoryDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var currentCustomerGroup = await _customerCategoryService.GetByRowGuidAsync(key);
            if (currentCustomerGroup == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<CustomerCategoryDto>(currentCustomerGroup);
            customerCategoryDto.Patch(dto);

            var entity = _mapper.Map(dto, currentCustomerGroup);

            await _customerCategoryService.UpdateAsync(entity);
            return Updated(_mapper.Map<CustomerCategoryDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _customerCategoryService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
