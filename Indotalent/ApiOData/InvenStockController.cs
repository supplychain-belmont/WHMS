using AutoMapper;

using Indotalent.Applications.InventoryTransactions;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using static System.Math;

namespace Indotalent.ApiOData
{
    public class InvenStockController : ODataController
    {
        private readonly InventoryTransactionService _transactionService;
        private readonly InventoryStockService _inventoryStockService;
        private readonly IMapper _mapper;

        public InvenStockController(InventoryTransactionService transactionService,
            InventoryStockService inventoryStockService, IMapper mapper)
        {
            _transactionService = transactionService;
            _inventoryStockService = inventoryStockService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<InvenStockDto> Get()
        {
            var transGrouped = _inventoryStockService.GetAll()
                .Where(data => data.Stock > 0)
                .Select(data => new InvenStockDto
                {
                    Id = data.Id,
                    RowGuid = data.RowGuid,
                    CreatedAtUtc = data.CreatedAtUtc,
                    WarehouseId = data.WarehouseId,
                    Warehouse = data.Warehouse,
                    ProductId = data.ProductId,
                    Product = data.Product,
                    Stock = data.Stock,
                    Reserved = data.Reserved,
                    ReservedPercentage = Round(Min(data.Reserved / data.Stock * 100, 100), 2),
                    Incoming = data.Incoming
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
            patchDoc.ApplyTo(dto, (error) => ModelState.AddModelError(string.Empty, error.ErrorMessage));

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
