using System.Linq;

using AutoMapper;

using Indotalent.Applications.InventoryTransactions;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class InvenTransController : ODataController
    {
        private readonly InventoryTransactionService _inventoryTransactionService;
        private readonly IMapper _mapper;

        public InvenTransController(InventoryTransactionService inventoryTransactionService, IMapper mapper)
        {
            _inventoryTransactionService = inventoryTransactionService;
            _mapper = mapper;
        }


        [EnableQuery]
        public IQueryable<InvenTransDto> Get()
        {
            return _inventoryTransactionService
                .GetAll()
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .Include(x => x.WarehouseFrom)
                .Include(x => x.WarehouseTo)
                .Where(x => x.Product!.Physical == true)
                .Select(rec => new InvenTransDto
                {
                    Id = rec.Id,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                    ModuleId = rec.ModuleId,
                    ModuleName = rec.ModuleName,
                    ModuleCode = rec.ModuleCode,
                    ModuleNumber = rec.ModuleNumber,
                    MovementDate = rec.MovementDate,
                    Status = rec.Status,
                    Number = rec.Number,
                    Warehouse = rec.Warehouse!.Name,
                    Product = rec.Product!.Name,
                    Movement = rec.Movement,
                    TransType = rec.TransType,
                    Stock = rec.Stock,
                    WarehouseFrom = rec.WarehouseFrom!.Name,
                    WarehouseTo = rec.WarehouseTo!.Name,
                });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _inventoryTransactionService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            await _inventoryTransactionService.DeleteByIdAsync(entity.Id);
            return NoContent();
        }
        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            var entity = await _inventoryTransactionService.GetByIdAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<InvenTransDto>(entity);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvenTransDto dto)
        {
            if (dto == null)
            {
                return BadRequest();
            }

            var entity = _mapper.Map<InventoryTransaction>(dto);
            await _inventoryTransactionService.AddAsync(entity);

            var createdDto = _mapper.Map<InvenTransDto>(entity);
            return Created(createdDto);
        }
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody] Delta<InventoryTransaction> patch)
        {
            var existingEntity = await _inventoryTransactionService.GetByIdAsync(key);
            if (existingEntity == null)
            {
                return NotFound();
            }

            patch.Patch(existingEntity);

            await _inventoryTransactionService.UpdateAsync(existingEntity);

            return Updated(existingEntity);
        }

    }
}
