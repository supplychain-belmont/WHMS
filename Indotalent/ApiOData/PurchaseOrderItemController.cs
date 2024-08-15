using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class PurchaseOrderItemController : ODataController
    {
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly IMapper _mapper;

        public PurchaseOrderItemController(PurchaseOrderItemService purchaseOrderItemService, IMapper mapper)
        {
            _purchaseOrderItemService = purchaseOrderItemService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<PurchaseOrderItemDto> Get()
        {
            return _purchaseOrderItemService
                .GetAll()
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .Include(x => x.Product)
                .ProjectTo<PurchaseOrderItemDto>(_mapper.ConfigurationProvider);
        }

        public async Task<ActionResult<PurchaseOrderItemDto>> Get([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _purchaseOrderItemService.GetByRowGuidAsync(key,
                    x => x.PurchaseOrder, x => x.Product)
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .ProjectTo<PurchaseOrderItemDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(vendor);
        }

        public async Task<ActionResult<PurchaseOrderItemDto>> Post([FromBody] PurchaseOrderItemDto purchaseOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = _mapper.Map<PurchaseOrderItem>(purchaseOrderItemDto);
            await _purchaseOrderItemService.AddAsync(vendor);
            return Created();
        }

        public async Task<ActionResult<PurchaseOrderItemDto>> Put([FromRoute] Guid key,
            [FromBody] PurchaseOrderItemDto purchaseOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentPurchaseOrderItem = await _purchaseOrderItemService.GetByRowGuidAsync(key);
            if (currentPurchaseOrderItem == null)
            {
                return NotFound();
            }

            _mapper.Map(purchaseOrderItemDto, currentPurchaseOrderItem);
            await _purchaseOrderItemService.UpdateAsync(currentPurchaseOrderItem);
            return NoContent();
        }

        public async Task<ActionResult<PurchaseOrderItemDto>> Patch([FromRoute] Guid key,
            [FromBody] Delta<PurchaseOrderItemDto> purchaseOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentPurchaseOrderItem = await _purchaseOrderItemService.GetByRowGuidAsync(key);
            if (currentPurchaseOrderItem == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<PurchaseOrderItemDto>(currentPurchaseOrderItem);
            purchaseOrderItemDto.Patch(dto);

            var entity = _mapper.Map(dto, currentPurchaseOrderItem);

            await _purchaseOrderItemService.UpdateAsync(entity);
            return Updated(_mapper.Map<PurchaseOrderItemDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] Guid key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _purchaseOrderItemService.DeleteByRowGuidAsync(key);
            return NoContent();
        }
    }
}
