using AutoMapper;
using Indotalent.Applications.Warehouses;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace Indotalent.ApiOData
{
    public class WarehouseController : ODataController
    {
        private readonly WarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public WarehouseController(WarehouseService warehouseService, IMapper mapper)
        {
            _warehouseService = warehouseService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<WarehouseDto> Get()
        {
            return _warehouseService
                .GetAll()
                .Select(rec => new WarehouseDto
                {
                    Id = rec.Id,
                    RowGuid = rec.RowGuid,
                    Name = rec.Name,
                    SystemWarehouse = rec.SystemWarehouse,
                    Description = rec.Description,
                    CreatedAtUtc = rec.CreatedAtUtc
                });
        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _warehouseService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<WarehouseDto>(entity);
            return Ok(dto);
        }

        public async Task<IActionResult> Post([FromBody] WarehouseDto dto)
        {
            var entity = _mapper.Map<Warehouse>(dto);
            await _warehouseService.AddAsync(entity);
            var createdDto = _mapper.Map<WarehouseDto>(entity);
            return Created(createdDto);
        }

        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] WarehouseDto dto)
        {
            if (key != dto.Id)
            {
                return BadRequest();
            }

            var entity = await _warehouseService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            _mapper.Map(dto, entity);
            await _warehouseService.UpdateAsync(entity);
            return Updated(_mapper.Map<WarehouseDto>(entity));
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] JsonPatchDocument<WarehouseDto> patch)
        {
            var entity = await _warehouseService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<WarehouseDto>(entity);
            patch.ApplyTo(dto);
            _mapper.Map(dto, entity);
            await _warehouseService.UpdateAsync(entity);
            return Updated(_mapper.Map<WarehouseDto>(entity));
        }

        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _warehouseService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _warehouseService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
