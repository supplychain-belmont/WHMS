using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.PurchaseOrders;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class PurchaseOrderController : ODataController
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly IMapper _mapper;

        public PurchaseOrderController(PurchaseOrderService purchaseOrderService, IMapper mapper)
        {
            _purchaseOrderService = purchaseOrderService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<PurchaseOrderDto> Get()
        {
            return _purchaseOrderService
                .GetAll()
                .Include(x => x.Vendor)
                .Include(x => x.Tax)
                .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<PurchaseOrderDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vendor = await _purchaseOrderService.GetByIdAsync(key,
                    x => x.Vendor, x => x.Tax)
                .ProjectTo<PurchaseOrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(vendor);
        }

        public async Task<ActionResult<PurchaseOrderDto>> Post([FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purchaseOrder = _mapper.Map<PurchaseOrder>(purchaseOrderDto);
            await _purchaseOrderService.AddAsync(purchaseOrder);
            return Created();
        }

        public async Task<ActionResult<PurchaseOrderDto>> Put([FromRoute] int key,
            [FromBody] PurchaseOrderDto purchaseOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentPurchaseOrder = await _purchaseOrderService.GetByIdAsync(key);
            if (currentPurchaseOrder == null)
            {
                return NotFound();
            }

            if (currentPurchaseOrder.Number != purchaseOrderDto.Number)
            {
                return BadRequest("Unable to update vendor");
            }

            _mapper.Map(purchaseOrderDto, currentPurchaseOrder);
            await _purchaseOrderService.UpdateAsync(currentPurchaseOrder);
            return NoContent();
        }

        public async Task<ActionResult<PurchaseOrderDto>> Patch([FromRoute] int key,
            [FromBody] Delta<PurchaseOrderDto> purchaseOrderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentVendor = await _purchaseOrderService.GetByIdAsync(key);
            if (currentVendor == null)
            {
                return NotFound();
            }

            purchaseOrderDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && currentVendor.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<PurchaseOrderDto>(currentVendor);
            purchaseOrderDto.Patch(dto);

            var entity = _mapper.Map(dto, currentVendor);

            await _purchaseOrderService.UpdateAsync(entity);
            return Updated(_mapper.Map<PurchaseOrderDto>(entity));
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _purchaseOrderService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
