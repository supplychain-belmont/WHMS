using AutoMapper;

using Indotalent.Applications.SalesOrders;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class SalesOrderController : ODataController
    {
        private readonly SalesOrderService _salesOrderService;

        public SalesOrderController(SalesOrderService salesOrderService)
        {
            _salesOrderService = salesOrderService;
        }

        [EnableQuery]
        public IQueryable<SalesOrderDto> Get()
        {
            return _salesOrderService.GetAllDtos();
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<SingleResult<SalesOrderDto>> Get([FromODataUri] int key)
        {
            var result = await _salesOrderService.GetDtoByIdAsync(key);
            return SingleResult.Create(result != null ? new[] { result }.AsQueryable() : Enumerable.Empty<SalesOrderDto>().AsQueryable());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SalesOrderDto salesOrderDto)
        {
            var result = await _salesOrderService.CreateAsync(salesOrderDto);
            return Created(result);
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] SalesOrderDto salesOrderDto)
        {
            if (key != salesOrderDto.Id)
            {
                return BadRequest();
            }

            var result = await _salesOrderService.UpdateAsync(salesOrderDto);
            return Updated(result);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            await _salesOrderService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
