using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.PurchaseOrders;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

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
        
        [HttpPost("AutoGenerate")]
        public async Task<ActionResult<PurchaseOrderDto>> AutoGenerate([FromBody] AutoPurchaseOrderDto dto)
        {
            var entity = await ((PurchaseOrderService)_service).AutoGeneratePurchaseOrderAsync(dto);
            var result = _mapper.Map<PurchaseOrderDto>(entity);
            return Ok(result);
        }
        [HttpPost("AutoPOTransferFlow")]
        public async Task<ActionResult<object>> AutoPOTransferFlow([FromBody] AutoPOTransferFlowDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (dto == null) return BadRequest("Body is null");
            if (dto.WarehouseId <= 0) return BadRequest("WarehouseId is required");

            var (po, to, ti, toLines, tiLines) =
                await ((PurchaseOrderService)_service).AutoGenerateWithTransfersAsync(dto);

            return Ok(new
            {
                PurchaseOrder = _mapper.Map<PurchaseOrderDto>(po),
                TransferOut   = _mapper.Map<TransferOutDto>(to),
                TransferIn    = _mapper.Map<TransferInDto>(ti),
                TransferOutLinesCreated = toLines,
                TransferInLinesCreated  = tiLines
            });
        }
    }
}
