using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.TransferOuts;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class TransferOutController : BaseODataController<TransferOut, TransferOutDto>
    {
        public TransferOutController(TransferOutService service, IMapper mapper) : base(service, mapper)
        {
        }

        public override IQueryable<TransferOutDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.WarehouseFrom)
                .Include(x => x.WarehouseTo)
                .ProjectTo<TransferOutDto>(_mapper.ConfigurationProvider);
        }

        public override Task<ActionResult<TransferOutDto>> Get(int key)
        {
            return GetEntityWithIncludesAsync(key, x => x.WarehouseFrom, x => x.WarehouseTo);
        }
    }
}
