using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.DeliveryOrders;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class DeliveryOrderController : ODataController
    {
        private readonly DeliveryOrderService _deliveryOrderService;
        private readonly IMapper _mapper;

        public DeliveryOrderController(DeliveryOrderService deliveryOrderService, IMapper mapper)
        {
            _deliveryOrderService = deliveryOrderService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<DeliveryOrderDto> Get()
        {
            return _deliveryOrderService
                .GetAll()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .ProjectTo<DeliveryOrderDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _deliveryOrderService
                .GetAll()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .ProjectTo<DeliveryOrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<DeliveryOrderDto>(entity);
            return Ok(dto);
        }

        public async Task<IActionResult> Post([FromBody] DeliveryOrderDto dto)
        {
            var entity = _mapper.Map<DeliveryOrder>(dto);
            await _deliveryOrderService.AddAsync(entity);
            return Created();
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] DeliveryOrderDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = await _deliveryOrderService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            await _deliveryOrderService.UpdateAsync(entity);
            return Updated(_mapper.Map<DeliveryOrderDto>(entity));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key,
            [FromBody] Delta<DeliveryOrderDto> deliveryOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _deliveryOrderService.GetByIdAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            deliveryOrderDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && current.Number != number)
            {
                return BadRequest("Unable to update delivery order");
            }

            var dto = _mapper.Map<DeliveryOrderDto>(current);
            deliveryOrderDto.Patch(dto);

            var entity = _mapper.Map(dto, current);

            await _deliveryOrderService.UpdateAsync(entity);
            return Updated(_mapper.Map<DeliveryOrderDto>(entity));
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _deliveryOrderService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _deliveryOrderService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
