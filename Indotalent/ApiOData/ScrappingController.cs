using AutoMapper;

using Indotalent.Applications.Scrappings;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class ScrappingController : ODataController
    {
        private readonly ScrappingService _scrappingService;
        private readonly IMapper _mapper;

        public ScrappingController(ScrappingService scrappingService, IMapper mapper)
        {
            _scrappingService = scrappingService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<ScrappingDto> Get()
        {
            return _scrappingService
                .GetAll()
                .Include(x => x.Warehouse)
                .Select(rec => new ScrappingDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    ScrappingDate = rec.ScrappingDate,
                    Status = rec.Status,
                    Warehouse = rec.Warehouse!.Name,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _scrappingService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ScrappingDto>(entity);
            return Ok(dto);
        }

        public async Task<IActionResult> Post([FromBody] ScrappingDto dto)
        {
            var entity = _mapper.Map<Scrapping>(dto);
            await _scrappingService.AddAsync(entity);
            var createdDto = _mapper.Map<ScrappingDto>(entity);
            return Created(createdDto);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] ScrappingDto dto)
        {
            if (key != dto.Id)
            {
                return BadRequest();
            }

            var entity = await _scrappingService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            await _scrappingService.UpdateAsync(entity);
            return Updated(_mapper.Map<ScrappingDto>(entity));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JsonPatchDocument<ScrappingDto> patch)
        {
            var entity = await _scrappingService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<ScrappingDto>(entity);
            patch.ApplyTo(dto);
            _mapper.Map(dto, entity);
            await _scrappingService.UpdateAsync(entity);
            return Updated(_mapper.Map<ScrappingDto>(entity));
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _scrappingService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _scrappingService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
