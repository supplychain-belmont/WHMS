using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.TransferIns;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Indotalent.ApiOData
{
    public class TransferInController : BaseODataController<TransferIn, TransferInDto>
    {
        public TransferInController(TransferInService service, IMapper mapper) : base(service, mapper)
        {
        }

        public override IQueryable<TransferInDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.TransferOut)
                .ThenInclude(x => x!.WarehouseFrom)
                .Include(x => x.TransferOut)
                .ThenInclude(x => x!.WarehouseTo)
                .ProjectTo<TransferInDto>(_mapper.ConfigurationProvider);
        }

        public override Task<ActionResult<TransferInDto>> Get(int key)
        {
            return GetEntityWithIncludesAsync(key, x => x.TransferOut);
        }
    }
}
