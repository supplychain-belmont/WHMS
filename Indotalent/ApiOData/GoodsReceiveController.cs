using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.GoodsReceives;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class GoodsReceiveController : BaseODataController<GoodsReceive, GoodsReceiveDto>
    {
        public GoodsReceiveController(GoodsReceiveService service, IMapper mapper) : base(service, mapper)
        {
        }

        public override IQueryable<GoodsReceiveDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .ProjectTo<GoodsReceiveDto>(_mapper.ConfigurationProvider);
        }
    }
}
