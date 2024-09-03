using AutoMapper;

using Indotalent.Applications.SalesOrderItems;
using Indotalent.DTOs;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;

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
            return _salesOrderItemService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<SalesOrderItemDto>> Get([FromODataUri] int key)
        {
            var result = await _salesOrderItemService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<SalesOrderItemDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesOrderItemDto salesOrderItemDto)
        {
            var result = await _salesOrderItemService.CreateAsync(salesOrderItemDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] SalesOrderItemDto salesOrderItemDto)
        {
            if (key != salesOrderItemDto.Id)
            {
                return BadRequest();
            }

            var result = await _salesOrderItemService.UpdateAsync(salesOrderItemDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _salesOrderItemService.DeleteByIdAsync(key);
            return NoContent();
        }
        
        [HttpPatch("{key}")]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JsonPatchDocument<SalesOrderItemDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var result = await _salesOrderItemService.PatchAsync(key, patchDoc);
            if (result == null)
            {
                return NotFound();
            }

            return Updated(result);
        }

    }
}
