using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.DeliveryOrders;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class DeliveryOrderController : BaseODataController<DeliveryOrder, DeliveryOrderDto>
    {
        private readonly DeliveryOrderService _deliveryOrderService;

        public DeliveryOrderController(DeliveryOrderService service, IMapper mapper) : base(service, mapper)
        {
            _deliveryOrderService = service;
        }

        public override IQueryable<DeliveryOrderDto> Get()
        {
            return _service
                .GetAll()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .ProjectTo<DeliveryOrderDto>(_mapper.ConfigurationProvider);
        }

        public override async Task<ActionResult<DeliveryOrderDto>> Get(int key)
        {
            var entity = await _deliveryOrderService
                .GetAll()
                .Include(x => x.SalesOrder)
                .ThenInclude(x => x!.Customer)
                .ProjectTo<DeliveryOrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> ProcessDeliveryOrder(ODataActionParameters actionParameters)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (actionParameters["salesOrderId"] is not int salesOrderId)
                {
                    return BadRequest("Deliver Order Id is required.");
                }

                var deliverOrderId = await _deliveryOrderService.ProcessGoodsReceiveAsync(salesOrderId);
                var deliverOrder = await _deliveryOrderService
                    .GetAll()
                    .Include(x => x.SalesOrder)
                    .ThenInclude(x => x!.Customer)
                    .FirstOrDefaultAsync(x => x.Id == deliverOrderId);
                return Ok(_mapper.Map<DeliveryOrderDto>(deliverOrder));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
        }
    }
}
