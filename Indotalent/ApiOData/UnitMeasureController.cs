using AutoMapper;
using Indotalent.Applications.UnitMeasures;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Indotalent.ApiOData
{
    public class UnitMeasureController : ODataController
    {
        private readonly UnitMeasureService _unitMeasureService;
        private readonly IMapper _mapper;

        public UnitMeasureController(UnitMeasureService unitMeasureService, IMapper mapper)
        {
            _unitMeasureService = unitMeasureService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<UnitMeasureDto> Get()
        {
            return _unitMeasureService
                .GetAll()
                .Select(rec => new UnitMeasureDto
                {
                    Id = rec.Id,
                    RowGuid = rec.RowGuid,
                    Name = rec.Name,
                    Description = rec.Description,
                    CreatedAtUtc = rec.CreatedAtUtc
                });
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _unitMeasureService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<UnitMeasureDto>(entity);
            return Ok(dto);
        }

        public async Task<IActionResult> Post([FromBody] UnitMeasureDto dto)
        {
            var entity = _mapper.Map<UnitMeasure>(dto);
            await _unitMeasureService.AddAsync(entity);
            var createdDto = _mapper.Map<UnitMeasureDto>(entity);
            return Created(createdDto);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] UnitMeasureDto dto)
        {
            if (key != dto.Id)
            {
                return BadRequest();
            }

            var entity = await _unitMeasureService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            await _unitMeasureService.UpdateAsync(entity);
            return Updated(_mapper.Map<UnitMeasureDto>(entity));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JsonPatchDocument<UnitMeasureDto> patch)
        {
            var entity = await _unitMeasureService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<UnitMeasureDto>(entity);
            patch.ApplyTo(dto);
            _mapper.Map(dto, entity);
            await _unitMeasureService.UpdateAsync(entity);
            return Updated(_mapper.Map<UnitMeasureDto>(entity));
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _unitMeasureService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _unitMeasureService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
