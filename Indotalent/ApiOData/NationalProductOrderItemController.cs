using Indotalent.Applications.NationalProductOrderItems;
using Indotalent.DTOs;
using Indotalent.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class NationalProductOrderItemController : ODataController
    {
        private readonly NationalProductOrderItemService _nationalProductOrderItemService;

        public NationalProductOrderItemController(NationalProductOrderItemService nationalProductOrderItemService)
        {
            _nationalProductOrderItemService = nationalProductOrderItemService;
        }

        [EnableQuery]
        public IQueryable<NationalProductOrderItemDto> Get()
        {
            return _nationalProductOrderItemService
                .GetAll()
                .Select(rec => new NationalProductOrderItemDto
                {
                    Id = rec.Id,
                    NationalProductOrderId = rec.NationalProductOrderId,
                    ProductId = rec.ProductId,
                    Summary = rec.Summary,
                    UnitPrice = rec.UnitPrice,
                    Quantity = rec.Quantity,
                    Total = rec.Total,
                    RowGuid = rec.RowGuid,
                    CreatedByUserId = rec.CreatedByUserId,
                    CreatedAtUtc = rec.CreatedAtUtc,
                    UpdatedByUserId = rec.UpdatedByUserId,
                    UpdatedAtUtc = rec.UpdatedAtUtc,
                    IsNotDeleted = rec.IsNotDeleted,
                    ManPowerCost = rec.ManPowerCost,
                    MaterialCost = rec.MaterialCost,
                    ShippingCost = rec.ShippingCost,
                    TotalAmount = rec.TotalAmount,
                    DiscountCost = rec.DiscountCost,
                    AmountPayable = rec.AmountPayable,
                    Utility1 = rec.Utility1,
                    Utility2 = rec.Utility2,
                    UnitPriceInvoice = rec.UnitPriceInvoice,
                    UnitPriceNoInvoice = rec.UnitPriceNoInvoice,
                    ColorCode = rec.ColorCode
                });
        }

        [EnableQuery]
        public SingleResult<NationalProductOrderItemDto> Get([FromODataUri] int key)
        {
            var result = _nationalProductOrderItemService
                .GetAll()
                .Where(n => n.Id == key)
                .Select(rec => new NationalProductOrderItemDto
                {
                    Id = rec.Id,
                    NationalProductOrderId = rec.NationalProductOrderId,
                    ProductId = rec.ProductId,
                    Summary = rec.Summary,
                    UnitPrice = rec.UnitPrice,
                    Quantity = rec.Quantity,
                    Total = rec.Total,
                    RowGuid = rec.RowGuid,
                    CreatedByUserId = rec.CreatedByUserId,
                    CreatedAtUtc = rec.CreatedAtUtc,
                    UpdatedByUserId = rec.UpdatedByUserId,
                    UpdatedAtUtc = rec.UpdatedAtUtc,
                    IsNotDeleted = rec.IsNotDeleted,
                    ManPowerCost = rec.ManPowerCost,
                    MaterialCost = rec.MaterialCost,
                    ShippingCost = rec.ShippingCost,
                    TotalAmount = rec.TotalAmount,
                    DiscountCost = rec.DiscountCost,
                    AmountPayable = rec.AmountPayable,
                    Utility1 = rec.Utility1,
                    Utility2 = rec.Utility2,
                    UnitPriceInvoice = rec.UnitPriceInvoice,
                    UnitPriceNoInvoice = rec.UnitPriceNoInvoice,
                    ColorCode = rec.ColorCode
                });

            return SingleResult.Create(result);
        }

        public async Task<IActionResult> Post([FromBody] NationalProductOrderItemDto nationalProductOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalProductOrderItem = new NationalProductOrderItem
            {
                NationalProductOrderId = nationalProductOrderItemDto.NationalProductOrderId,
                ProductId = nationalProductOrderItemDto.ProductId,
                Summary = nationalProductOrderItemDto.Summary,
                UnitPrice = nationalProductOrderItemDto.UnitPrice,
                Quantity = nationalProductOrderItemDto.Quantity,
                Total = nationalProductOrderItemDto.Total,
                RowGuid = nationalProductOrderItemDto.RowGuid,
                CreatedByUserId = nationalProductOrderItemDto.CreatedByUserId,
                CreatedAtUtc = nationalProductOrderItemDto.CreatedAtUtc,
                UpdatedByUserId = nationalProductOrderItemDto.UpdatedByUserId,
                UpdatedAtUtc = nationalProductOrderItemDto.UpdatedAtUtc,
                IsNotDeleted = nationalProductOrderItemDto.IsNotDeleted,
                ManPowerCost = nationalProductOrderItemDto.ManPowerCost,
                MaterialCost = nationalProductOrderItemDto.MaterialCost,
                ShippingCost = nationalProductOrderItemDto.ShippingCost,
                TotalAmount = nationalProductOrderItemDto.TotalAmount,
                DiscountCost = nationalProductOrderItemDto.DiscountCost,
                AmountPayable = nationalProductOrderItemDto.AmountPayable,
                Utility1 = nationalProductOrderItemDto.Utility1,
                Utility2 = nationalProductOrderItemDto.Utility2,
                UnitPriceInvoice = nationalProductOrderItemDto.UnitPriceInvoice,
                UnitPriceNoInvoice = nationalProductOrderItemDto.UnitPriceNoInvoice,
                ColorCode = nationalProductOrderItemDto.ColorCode
            };

            await _nationalProductOrderItemService.AddAsync(nationalProductOrderItem);
            return Created(nationalProductOrderItem);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] NationalProductOrderItemDto nationalProductOrderItemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nationalProductOrderItem = await _nationalProductOrderItemService.GetByIdAsync(key);
            if (nationalProductOrderItem == null)
            {
                return NotFound();
            }

            nationalProductOrderItem.NationalProductOrderId = nationalProductOrderItemDto.NationalProductOrderId;
            nationalProductOrderItem.ProductId = nationalProductOrderItemDto.ProductId;
            nationalProductOrderItem.Summary = nationalProductOrderItemDto.Summary;
            nationalProductOrderItem.UnitPrice = nationalProductOrderItemDto.UnitPrice;
            nationalProductOrderItem.Quantity = nationalProductOrderItemDto.Quantity;
            nationalProductOrderItem.Total = nationalProductOrderItemDto.Total;
            nationalProductOrderItem.RowGuid = nationalProductOrderItemDto.RowGuid;
            nationalProductOrderItem.CreatedByUserId = nationalProductOrderItemDto.CreatedByUserId;
            nationalProductOrderItem.CreatedAtUtc = nationalProductOrderItemDto.CreatedAtUtc;
            nationalProductOrderItem.UpdatedByUserId = nationalProductOrderItemDto.UpdatedByUserId;
            nationalProductOrderItem.UpdatedAtUtc = nationalProductOrderItemDto.UpdatedAtUtc;
            nationalProductOrderItem.IsNotDeleted = nationalProductOrderItemDto.IsNotDeleted;
            nationalProductOrderItem.ManPowerCost = nationalProductOrderItemDto.ManPowerCost;
            nationalProductOrderItem.MaterialCost = nationalProductOrderItemDto.MaterialCost;
            nationalProductOrderItem.ShippingCost = nationalProductOrderItemDto.ShippingCost;
            nationalProductOrderItem.TotalAmount = nationalProductOrderItemDto.TotalAmount;
            nationalProductOrderItem.DiscountCost = nationalProductOrderItemDto.DiscountCost;
            nationalProductOrderItem.AmountPayable = nationalProductOrderItemDto.AmountPayable;
            nationalProductOrderItem.Utility1 = nationalProductOrderItemDto.Utility1;
            nationalProductOrderItem.Utility2 = nationalProductOrderItemDto.Utility2;
            nationalProductOrderItem.UnitPriceInvoice = nationalProductOrderItemDto.UnitPriceInvoice;
            nationalProductOrderItem.UnitPriceNoInvoice = nationalProductOrderItemDto.UnitPriceNoInvoice;
            nationalProductOrderItem.ColorCode = nationalProductOrderItemDto.ColorCode;

            await _nationalProductOrderItemService.UpdateAsync(nationalProductOrderItem);
            return Updated(nationalProductOrderItem);
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var nationalProductOrderItem = await _nationalProductOrderItemService.GetByIdAsync(key);
            if (nationalProductOrderItem == null)
            {
                return NotFound();
            }

            await _nationalProductOrderItemService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
