using System.Linq;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.AdjustmentMinuss;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class NegativeAdjustmentController : ODataController
    {
        private readonly AdjustmentMinusService _adjustmentMinusService;
        private readonly IMapper _mapper;

        public NegativeAdjustmentController(AdjustmentMinusService adjustmentMinusService, IMapper mapper)
        {
            _adjustmentMinusService = adjustmentMinusService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<NegativeAdjustmentDto> Get()
        {
            return _adjustmentMinusService
                .GetAll()
                .Select(rec => new NegativeAdjustmentDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    AdjustmentDate = rec.AdjustmentDate,
                    Status = rec.Status,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }

        public async Task<ActionResult<NegativeAdjustmentDto>> Post([FromBody] NegativeAdjustmentDto adjustmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adjustment = _mapper.Map<AdjustmentMinus>(adjustmentDto);
            await _adjustmentMinusService.AddAsync(adjustment);
            return CreatedAtAction(nameof(Get), new { key = adjustment.Id }, adjustmentDto);
        }

        public async Task<ActionResult> Put([FromRoute] int key, [FromBody] NegativeAdjustmentDto adjustmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentAdjustment = await _adjustmentMinusService.GetByIdAsync(key);
            if (currentAdjustment == null)
            {
                return NotFound();
            }

            _mapper.Map(adjustmentDto, currentAdjustment);
            await _adjustmentMinusService.UpdateAsync(currentAdjustment);
            return NoContent();
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _adjustmentMinusService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
