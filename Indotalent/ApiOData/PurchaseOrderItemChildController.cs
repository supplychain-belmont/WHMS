using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Applications.PurchaseOrders;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class PurchaseOrderItemChildController : ODataController
    {
        private const string HeaderKeyName = "ParentId";
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly PurchaseOrderItemService _purchaseOrderItemService;
        private readonly ProductService _productService;
        private readonly IMapper _mapper;

        public PurchaseOrderItemChildController(
            PurchaseOrderService purchaseOrderService,
            IMapper mapper,
            PurchaseOrderItemService purchaseOrderItemService, ProductService productService)
        {
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
            _purchaseOrderItemService = purchaseOrderItemService;
            _productService = productService;
        }

        [EnableQuery]
        public IQueryable<PurchaseOrderItemChildDto> Get()
        {
            Request.Headers.TryGetValue(HeaderKeyName, out var headerValue);
            var parentId = int.Parse(headerValue.ToString());

            return _purchaseOrderItemService
                .GetAll()
                .Include(x => x.Product)
                .Where(x => x.PurchaseOrderId == parentId)
                .ProjectTo<PurchaseOrderItemChildDto>(_mapper.ConfigurationProvider);
        }


        [EnableQuery]
        [HttpGet("{key}")]
        public SingleResult<PurchaseOrderItemChildDto> Get([FromODataUri] int key)
        {
            return SingleResult.Create(_purchaseOrderItemService
                .GetAll()
                .Where(x => x.Id == key)
                .ProjectTo<PurchaseOrderItemChildDto>(_mapper.ConfigurationProvider));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key,
            [FromBody] Delta<PurchaseOrderItemChildDto> delta)
        {
            try
            {
                var purchaseOrderItem = await _purchaseOrderItemService
                    .GetAll()
                    .Include(x => x.Product)
                    .Where(x => x.Id == key)
                    .FirstOrDefaultAsync();

                if (purchaseOrderItem == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<PurchaseOrderItemChildDto>(purchaseOrderItem);
                delta.Patch(dto);
                var entity = _mapper.Map(dto, purchaseOrderItem);
                await _purchaseOrderItemService.UpdateAsync(entity);

                return Ok(_mapper.Map<PurchaseOrderItemChildDto>(entity));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PurchaseOrderItemChildDto purchaseOrderItem)
        {
            try
            {
                Request.Headers.TryGetValue(HeaderKeyName, out var headerValue);
                var parentId = int.Parse(headerValue.ToString());

                var product = await _productService.GetByIdAsync(purchaseOrderItem.ProductId);

                purchaseOrderItem.PurchaseOrderId = parentId;
                purchaseOrderItem.Summary = product!.Number;
                purchaseOrderItem.M3 = product!.M3;
                purchaseOrderItem.Quantity = 1;
                purchaseOrderItem.UnitCostDiscounted = purchaseOrderItem.UnitCost;

                var entity = _mapper.Map<PurchaseOrderItem>(purchaseOrderItem);
                await _purchaseOrderItemService.AddAsync(entity);

                var dto = _mapper.Map<PurchaseOrderItemDto>(entity);
                return Created("PurchaseOrderItemChild", dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            try
            {
                var purchaseOrderItem = await _purchaseOrderItemService.GetAll()
                    .Where(x => x.Id == key)
                    .FirstOrDefaultAsync();

                if (purchaseOrderItem == null)
                {
                    return BadRequest();
                }

                await _purchaseOrderItemService.DeleteByIdAsync(purchaseOrderItem.Id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
