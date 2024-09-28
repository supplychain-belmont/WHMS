using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.SalesOrderItems;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class SalesOrderItemController : ODataController
    {
        private readonly SalesOrderItemService _salesOrderItemService;
        private readonly IMapper _mapper;

        public SalesOrderItemController(SalesOrderItemService salesOrderItemService, IMapper mapper)
        {
            _salesOrderItemService = salesOrderItemService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<SalesOrderItemDto> Get()
        {
            return _salesOrderItemService.GetAll()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .Include(x => x.Product)
                .ProjectTo<SalesOrderItemDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<SalesOrderItemDto>> Get([FromODataUri] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _salesOrderItemService.GetByRowGuidAsync(key,
                    x => x.SalesOrder, x => x.Product)
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .ProjectTo<SalesOrderItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesOrderItemDto salesOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = _mapper.Map<SalesOrderItem>(salesOrderItemDto);
            await _salesOrderItemService.AddAsync(order);
            return Created();
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] SalesOrderItemDto salesOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _salesOrderItemService.GetByRowGuidAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            _mapper.Map(salesOrderItemDto, current);
            await _salesOrderItemService.UpdateAsync(current);
            return NoContent();
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _salesOrderItemService.DeleteByRowGuidAsync(key);
            return NoContent();
        }

        [HttpPatch("{key}")]
        public async Task<IActionResult> Patch([FromODataUri] Guid key,
            [FromBody] Delta<SalesOrderItemDto> patchDoc)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _salesOrderItemService.GetByRowGuidAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<SalesOrderItemDto>(current);
            patchDoc.Patch(dto);

            var entity = _mapper.Map(dto, current);

            await _salesOrderItemService.UpdateAsync(entity);
            return Updated(_mapper.Map<SalesOrderItemDto>(entity));
        }
    }
}
