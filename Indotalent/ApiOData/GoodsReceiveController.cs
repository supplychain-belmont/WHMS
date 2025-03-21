using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.GoodsReceives;
using Indotalent.Applications.NumberSequences;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
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
        private readonly NumberSequenceService _numberSequenceService;

        public GoodsReceiveController(GoodsReceiveService goodsReceiveService, IMapper mapper,
            NumberSequenceService numberSequenceService)
        {
            _goodsReceiveService = goodsReceiveService;
            _mapper = mapper;
            _numberSequenceService = numberSequenceService;
        }

        [EnableQuery]
        public IQueryable<GoodsReceiveDto> Get()
        {
            return _goodsReceiveService
                .GetAll()
                .Include(x => x.PurchaseOrder)
                .ThenInclude(x => x!.Vendor)
                .ProjectTo<GoodsReceiveDto>(_mapper.ConfigurationProvider);
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
            entity.Number = _numberSequenceService.GenerateNumber(nameof(GoodsReceive), "", "GR");

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
        public async Task<IActionResult> Patch([FromODataUri] int key,
            [FromBody] Delta<GoodsReceiveDto> goodReceiveDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var current = await _goodsReceiveService.GetByIdAsync(key);
            if (current == null)
            {
                return NotFound();
            }

            goodReceiveDto.TryGetPropertyValue("Number", out var numberProperty);
            if (numberProperty is string number && current.Number != number)
            {
                return BadRequest("Unable to update vendor");
            }

            var dto = _mapper.Map<GoodsReceiveDto>(current);
            goodReceiveDto.Patch(dto);

            var entity = _mapper.Map(dto, current);

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
