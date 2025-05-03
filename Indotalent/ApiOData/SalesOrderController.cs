using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.SalesOrders;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;
using Indotalent.Infrastructures.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class SalesOrderController : BaseODataController<SalesOrder, SalesOrderDto>
    {
        public SalesOrderController(SalesOrderService service, IMapper mapper) : base(service, mapper)
        {
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
    }
}
