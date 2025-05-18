using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.Applications.PurchaseOrderItems;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace Indotalent.ApiOData
{
    public class
        PurchaseOrderItemChildController : BaseODataItemController<PurchaseOrderItem, PurchaseOrderItemChildDto>
    {
        private readonly ProductService _productService;

        public PurchaseOrderItemChildController(PurchaseOrderItemService service, ProductService productService,
            IMapper mapper) : base(service, mapper)
        {
            _productService = productService;
        }

        public override IQueryable<PurchaseOrderItemChildDto> Get()
        {
            var parentId = SetParentId();
            return GetEntityWithIncludes(x => x.PurchaseOrderId == parentId, x => x.Product)
                .ProjectTo<PurchaseOrderItemChildDto>(_mapper.ConfigurationProvider);
        }

        public override async Task<ActionResult<PurchaseOrderItemChildDto>> Post(
            [FromBody] PurchaseOrderItemChildDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentId = SetParentId();
            var product = await _productService.GetByIdAsync(dto.ProductId);

            dto.PurchaseOrderId = parentId;
            dto.Summary = product!.Number;
            dto.M3 = product!.M3;
            dto.Quantity = 1;
            dto.UnitCostDiscounted = dto.UnitCost;
            return await base.Post(dto);
        }
    }
}
