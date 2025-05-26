using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.SalesOrders;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class SalesOrderController : BaseODataController<SalesOrder, SalesOrderDto>
    {
        private readonly SalesOrderService _salesOrderService;

        public SalesOrderController(SalesOrderService service, IMapper mapper) : base(service, mapper)
        {
            _salesOrderService = service;
        }

        public override IQueryable<SalesOrderDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.Customer)
                .Include(x => x.Tax)
                .ProjectTo<SalesOrderDto>(_mapper.ConfigurationProvider);
        }

        public override async Task<ActionResult<SalesOrderDto>> Get(int key)
        {
            return await GetEntityWithIncludesAsync(key,
                x => x.Customer, x => x.Customer, x => x.Tax);
        }

        [HttpPost]
        public async Task<IActionResult> OrderFromAssembly(ODataActionParameters actionParameters)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (actionParameters["AssemblyId"] is not int assemblyId)
            {
                return BadRequest("AssemblyId is required.");
            }

            var order = await _salesOrderService.CreateOrderFromAssemblyAsync(assemblyId);

            return Ok(_mapper.Map<SalesOrderDto>(order));
        }
    }
}
