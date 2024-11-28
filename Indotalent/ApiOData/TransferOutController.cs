using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.TransferOuts;
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
                .ProjectTo<TransferOutDto>(_mapper.ConfigurationProvider);
        }

        [EnableQuery]
        [HttpGet("{key}")]
        public async Task<ActionResult<TransferOutDto>> Get([FromODataUri] int key)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transferOut = await _transferOutService
                .GetByIdAsync(key, x => x.WarehouseFrom, x => x.WarehouseTo)
                .ProjectTo<TransferOutDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
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

        public async Task<ActionResult<TransferOutDto>> Put([FromRoute] int key,
            [FromBody] TransferOutDto transferOutDto)
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

        public async Task<ActionResult<TransferOutDto>> Patch([FromRoute] int key,
            [FromBody] Delta<TransferOutDto> transferOutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var order = await _transferOutService.GetByIdAsync(key);
                if (order == null)
                {
                    return NotFound();
                }

                var dto = _mapper.Map<TransferOutDto>(order);
                transferOutDto.Patch(dto);

                var entity = _mapper.Map(dto, order);

                await _transferOutService.UpdateAsync(entity);
                return Updated(_mapper.Map<TransferOutDto>(entity));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.Message);
            }
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
