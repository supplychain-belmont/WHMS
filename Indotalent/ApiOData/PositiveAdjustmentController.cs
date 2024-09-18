using AutoMapper;
using Indotalent.Applications.AdjustmentPluss;
using Indotalent.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using Indotalent.Models.Entities;

namespace Indotalent.ApiOData
{
    public class PositiveAdjustmentController : ODataController
    {
        private readonly AdjustmentPlusService _adjustmentPlusService;
        private readonly IMapper _mapper;

        public PositiveAdjustmentController(AdjustmentPlusService adjustmentPlusService, IMapper mapper)
        {
            _adjustmentPlusService = adjustmentPlusService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<PositiveAdjustmentDto> Get()
        {
            return _adjustmentPlusService
                .GetAll()
                .Select(rec => new PositiveAdjustmentDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    AdjustmentDate = rec.AdjustmentDate,
                    Status = rec.Status,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }

        public async Task<ActionResult<PositiveAdjustmentDto>> Post([FromBody] PositiveAdjustmentDto adjustmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var adjustment = _mapper.Map<AdjustmentPlus>(adjustmentDto);
            await _adjustmentPlusService.AddAsync(adjustment);
            return CreatedAtAction(nameof(Get), new { key = adjustment.Id }, adjustmentDto);
        }

        public async Task<ActionResult> Put([FromRoute] int key, [FromBody] PositiveAdjustmentDto adjustmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentAdjustment = await _adjustmentPlusService.GetByIdAsync(key);
            if (currentAdjustment == null)
            {
                return NotFound();
            }

            _mapper.Map(adjustmentDto, currentAdjustment);
            await _adjustmentPlusService.UpdateAsync(currentAdjustment);
            return NoContent();
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _adjustmentPlusService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
