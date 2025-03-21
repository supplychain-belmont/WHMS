using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.SalesOrders;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class SalesOrderController : ODataController
    {
        private readonly SalesOrderService _salesOrderService;
        private readonly IMapper _mapper;

        public SalesOrderController(SalesOrderService salesOrderService, IMapper mapper)
        {
            _salesOrderService = salesOrderService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<SalesOrderDto> Get()
        {
            return _salesOrderService
                .GetAll()
                .Include(x => x.Customer)
                .Include(x => x.Tax)
                .ProjectTo<SalesOrderDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<SalesOrderDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrder = await _salesOrderService.GetByIdAsync(key,
                    x => x.Customer, x => x.Tax)
                .ProjectTo<SalesOrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(salesOrder);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesOrderDto salesOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var salesOrder = _mapper.Map<SalesOrder>(salesOrderDto);
            await _salesOrderService.AddAsync(salesOrder);
            return Created();
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] SalesOrderDto salesOrderDto)
        {
            if (key != salesOrderDto.Id)
            {
                return BadRequest();
            }

            var current = await _salesOrderService.GetByIdAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            if (current.Number != salesOrderDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(salesOrderDto, current);
            await _salesOrderService.UpdateAsync(current);
            return NoContent();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _salesOrderService.DeleteByIdAsync(key);
            return NoContent();
        }

        public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<SalesOrderDto> patchDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _salesOrderService.GetByIdAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            patchDoc.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && current.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<SalesOrderDto>(current);
            patchDoc.Patch(dto);

            var entity = _mapper.Map(dto, current);

            await _salesOrderService.UpdateAsync(entity);
            return Updated(_mapper.Map<PurchaseOrderDto>(entity));
        }
    }
}
