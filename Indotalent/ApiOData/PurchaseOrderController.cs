using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.PurchaseOrders;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class PurchaseOrderController : BaseODataController<PurchaseOrder, PurchaseOrderDto>
    {
        public PurchaseOrderController(PurchaseOrderService service, IMapper mapper) : base(service, mapper)
        {
        }

        public override IQueryable<PurchaseOrderDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.Vendor)
                .Include(x => x.Tax)
                .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider);
        }

        public override async Task<ActionResult<PurchaseOrderDto>> Get(int key)
        {
            return await GetEntityWithIncludesAsync(key, x => x.Vendor, x => x.Tax);
        }
    }
}
