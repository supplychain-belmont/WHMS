using Indotalent.Applications.InventoryTransactions;
using Indotalent.DTOs;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using AutoMapper;
using Indotalent.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class InvenStockController : ODataController
    {
        private readonly InventoryTransactionService _transactionService;
        private readonly IMapper _mapper;

        public InvenStockController(InventoryTransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<InvenStockDto> Get()
        {
            var transGrouped = _transactionService
                .GetAll()
                .Include(x => x.Warehouse)
                .Include(x => x.Product)
                .Where(x =>
                    x.Status >= Models.Enums.InventoryTransactionStatus.Confirmed &&
                    x.Warehouse!.SystemWarehouse == false &&
                    x.Product!.Physical == true
                )
                .GroupBy(x => new { x.WarehouseId, x.ProductId })
                .Select(group => new InvenStockDto
                {
                    WarehouseId = group.Key.WarehouseId,
                    ProductId = group.Key.ProductId,
                    Warehouse = group.Max(x => x.Warehouse!.Name),
                    Product = group.Max(x => x.Product!.Name),
                    Stock = group.Sum(x => x.Stock),
                    Id = group.Max(x => x.Id),
                    RowGuid = group.Max(x => x.RowGuid),
                    CreatedAtUtc = group.Max(x => x.CreatedAtUtc)
                });

            return transGrouped;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetById(int key)
        {
            var transaction = await _transactionService.GetByIdAsync(key);
            if (transaction == null) return NotFound();

            var dto = _mapper.Map<InvenStockDto>(transaction);
            return Ok(dto);
        }

        [HttpPatch("{key}")]
        public async Task<IActionResult> Patch(int key, [FromBody] JsonPatchDocument<InvenStockDto> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var transaction = await _transactionService.GetByIdAsync(key);
            if (transaction == null) return NotFound();

            var dto = _mapper.Map<InvenStockDto>(transaction);
            patchDoc.ApplyTo(dto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _mapper.Map(dto, transaction);
            await _transactionService.UpdateAsync(transaction);

            return Updated(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InvenStockDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var transaction = _mapper.Map<InventoryTransaction>(dto);
            await _transactionService.AddAsync(transaction);

            return Created(transaction);
        }

        [HttpDelete("{key}")]
        public async Task<IActionResult> Delete(int key)
        {
            var transaction = await _transactionService.GetByIdAsync(key);
            if (transaction == null) return NotFound();

            await _transactionService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
