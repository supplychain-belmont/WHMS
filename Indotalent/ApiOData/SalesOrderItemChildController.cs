using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.Applications.SalesOrderItems;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace Indotalent.ApiOData
{
    public class SalesOrderItemChildController : BaseODataItemController<SalesOrderItem, SalesOrderItemChildDto>
    {
        private readonly ProductService _productService;

        public SalesOrderItemChildController(SalesOrderItemService service, ProductService productService,
            IMapper mapper) : base(service, mapper)
        {
            _productService = productService;
        }

        public override IQueryable<SalesOrderItemChildDto> Get()
        {
            var parentId = SetParentId();
            return _service
                .GetAll()
                .Where(x => x.SalesOrderId == parentId)
                .ProjectTo<SalesOrderItemChildDto>(_mapper.ConfigurationProvider);
        }

        public override async Task<ActionResult<SalesOrderItemChildDto>> Post(SalesOrderItemChildDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var parentId = SetParentId();
            var product = await _productService.GetByIdAsync(dto.ProductId);

            dto.SalesOrderId = parentId;
            dto.Summary = product?.Number;
            dto.UnitPrice = product!.UnitPrice;
            dto.Quantity = 1;
            return await base.Post(dto);
        }
    }
}
