using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.TransferIns;
using Indotalent.DTOs;
using Indotalent.Models.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
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
                .ProjectTo<TransferInDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<TransferInDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferIn = await _transferInService
                .GetByIdAsync(key, x => x.TransferOut)
                .ProjectTo<TransferInDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
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

        public async Task<ActionResult<TransferInDto>> Patch([FromRoute] int key,
            [FromBody] Delta<TransferInDto> transferOutDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _transferInService.GetByIdAsync(key);
            if (order == null)
            {
                return NotFound();
            }

            var dto = _mapper.Map<TransferInDto>(order);
            transferOutDto.Patch(dto);

            var entity = _mapper.Map(dto, order);

            await _transferInService.UpdateAsync(entity);
            return Updated(_mapper.Map<TransferInDto>(entity));
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
