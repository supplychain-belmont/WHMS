using AutoMapper;

using Indotalent.Applications.TransferIns;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;


namespace Indotalent.ApiOData
{
    public class TransferInController : ODataController
    {
        private readonly TransferInService _transferInService;
        private readonly IMapper _mapper;

        public TransferInController(TransferInService transferInService, IMapper mapper)
        {
            _transferInService = transferInService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<TransferInDto> Get()
        {
            return _transferInService
                .GetAll()
                .Include(x => x.TransferOut)
                    .ThenInclude(x => x!.WarehouseFrom)
                .Include(x => x.TransferOut)
                    .ThenInclude(x => x!.WarehouseTo)
                .Select(rec => new TransferInDto
                {
                    Id = rec.Id,
                    Number = rec.Number,
                    ReceiveDate = rec.TransferReceiveDate,
                    Status = rec.Status,
                    TransferOut = rec.TransferOut!.Number,
                    ReleaseDate = rec.TransferOut!.TransferReleaseDate,
                    WarehouseFrom = rec.TransferOut!.WarehouseFrom!.Name,
                    WarehouseTo = rec.TransferOut!.WarehouseTo!.Name,
                    RowGuid = rec.RowGuid,
                    CreatedAtUtc = rec.CreatedAtUtc,
                });
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<TransferInDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferIn = _transferInService.GetByIdAsync(key, x => x.TransferOut);
            if (transferIn == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TransferInDto>(transferIn);
            return Ok(dto);
        }

        public async Task<ActionResult<TransferInDto>> Post([FromBody] TransferInDto transferInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferIn = _mapper.Map<TransferIn>(transferInDto);
            await _transferInService.AddAsync(transferIn);
            return Created();
        }

        public async Task<ActionResult<TransferInDto>> Put([FromRoute] int key, [FromBody] TransferInDto transferInDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTransferIn = await _transferInService.GetByIdAsync(key);
            if (currentTransferIn == null)
            {
                return NotFound();
            }

            if (currentTransferIn.Number != transferInDto.Number)
            {
                return BadRequest("Unable to update transfer number.");
            }

            _mapper.Map(transferInDto, currentTransferIn);
            await _transferInService.UpdateAsync(currentTransferIn);
            return NoContent();
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _transferInService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
