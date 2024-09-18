using Indotalent.Applications.TransferOuts;
using Indotalent.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.OData.Formatter;

namespace Indotalent.ApiOData
{
    public class TransferOutController : ODataController
    {
        private readonly TransferOutService _transferOutService;
        private readonly IMapper _mapper;

        public TransferOutController(TransferOutService transferOutService, IMapper mapper)
        {
            _transferOutService = transferOutService;
            _mapper = mapper;
        }

        [EnableQuery]
        public IQueryable<TransferOutDto> Get()
        {
            return _transferOutService
                .GetAll()
                .Include(x => x.WarehouseFrom)
                .Include(x => x.WarehouseTo)
                .Select(rec => _mapper.Map<TransferOutDto>(rec));
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<TransferOutDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferOut = _transferOutService.GetByIdAsync(key, x => x.WarehouseFrom, x => x.WarehouseTo);
            if (transferOut == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TransferOutDto>(transferOut);
            return Ok(dto);
        }

        public async Task<ActionResult<TransferOutDto>> Post([FromBody] TransferOutDto transferOutDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferOut = _mapper.Map<TransferOut>(transferOutDto);
            await _transferOutService.AddAsync(transferOut);
            var createdDto = _mapper.Map<TransferOutDto>(transferOut);

            return Created(createdDto);
        }

        public async Task<ActionResult<TransferOutDto>> Put([FromRoute] int key, [FromBody] TransferOutDto transferOutDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentTransferOut = await _transferOutService.GetByIdAsync(key);
            if (currentTransferOut == null)
            {
                return NotFound();
            }

            if (currentTransferOut.Number != transferOutDto.Number)
            {
                return BadRequest("Unable to update transfer number.");
            }

            _mapper.Map(transferOutDto, currentTransferOut);
            await _transferOutService.UpdateAsync(currentTransferOut);
            return NoContent();
        }

        public async Task<ActionResult> Delete([FromRoute] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _transferOutService.DeleteByIdAsync(key);
            return NoContent();
        }
    }
}
