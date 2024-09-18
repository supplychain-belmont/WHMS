using AutoMapper;

using Indotalent.Applications.GoodsReceives;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class GoodsReceiveController : ODataController
    {
        private readonly GoodsReceiveService _goodsReceiveService;
        private readonly IMapper _mapper;

        public GoodsReceiveController(GoodsReceiveService goodsReceiveService, IMapper mapper)
        {
            _goodsReceiveService = goodsReceiveService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<GoodsReceiveDto> Get()
        {
            return _goodsReceiveService
                .GetAll()
                .Include(x => x.PurchaseOrder)
                    .ThenInclude(x => x!.Vendor)
                .Select(rec => new GoodsReceiveDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    ReceiveDate = rec.ReceiveDate,
                    Status = rec.Status,
                    PurchaseOrder = rec.PurchaseOrder!.Number,
                    OrderDate = rec.PurchaseOrder!.OrderDate,
                    Vendor = rec.PurchaseOrder!.Vendor!.Name,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _goodsReceiveService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<GoodsReceiveDto>(entity);
            return Ok(dto);
        }

        public async Task<IActionResult> Post([FromBody] GoodsReceiveDto dto)
        {
            var entity = _mapper.Map<GoodsReceive>(dto);
            await _goodsReceiveService.AddAsync(entity);
            var createdDto = _mapper.Map<GoodsReceiveDto>(entity);
            return Created(createdDto);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] GoodsReceiveDto dto)
        {
            if (key != dto.Id)
            {
                return BadRequest();
            }

            var entity = await _goodsReceiveService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            await _goodsReceiveService.UpdateAsync(entity);
            return Updated(_mapper.Map<GoodsReceiveDto>(entity));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JsonPatchDocument<GoodsReceiveDto> patch)
        {
            var entity = await _goodsReceiveService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<GoodsReceiveDto>(entity);
            patch.ApplyTo(dto);
            _mapper.Map(dto, entity);
            await _goodsReceiveService.UpdateAsync(entity);
            return Updated(_mapper.Map<GoodsReceiveDto>(entity));
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _goodsReceiveService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _goodsReceiveService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
