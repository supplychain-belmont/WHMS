using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.StockCounts;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData
{
    public class StockCountController : ODataController
    {
        private readonly StockCountService _stockCountService;
        private readonly IMapper _mapper;

        public StockCountController(StockCountService stockCountService, IMapper mapper)
        {
            _stockCountService = stockCountService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<StockCountDto> Get()
        {
            return _stockCountService
                .GetAll()
                .Include(x => x.Warehouse)
                .Select(rec => new StockCountDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    CountDate = rec.CountDate,
                    Status = rec.Status,
                    Warehouse = rec.Warehouse!.Name,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }


        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<StockCountDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockCount = await _stockCountService
                .GetByIdAsync(key, x => x.Warehouse)
                .ProjectTo<StockCountDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

            return Ok(stockCount);
        }


        public async Task<ActionResult<StockCountDto>> Post([FromBody] StockCountDto stockCountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stockCount = _mapper.Map<StockCount>(stockCountDto);
            await _stockCountService.AddAsync(stockCount);
            return CreatedAtAction(nameof(Get), new { key = stockCount.Id }, _mapper.Map<StockCountDto>(stockCount));
        }

        public async Task<ActionResult<StockCountDto>> Put([FromRoute] int key, [FromBody] StockCountDto stockCountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentStockCount = await _stockCountService.GetByIdAsync(key);
            if (currentStockCount == null)
            {
                return NotFound();
            }

            _mapper.Map(stockCountDto, currentStockCount);
            await _stockCountService.UpdateAsync(currentStockCount);
            return NoContent();
        }

        public async Task<ActionResult<StockCountDto>> Patch([FromRoute] int key, [FromBody] Delta<StockCountDto> stockCountDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentStockCount = await _stockCountService.GetByIdAsync(key);
            if (currentStockCount == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<StockCountDto>(currentStockCount);
            stockCountDto.Patch(dto);
            _mapper.Map(dto, currentStockCount);

            await _stockCountService.UpdateAsync(currentStockCount);
            return Updated(_mapper.Map<StockCountDto>(currentStockCount));
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _stockCountService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
