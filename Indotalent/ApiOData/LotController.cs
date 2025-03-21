using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Lots;
using Indotalent.DTOs;
using Indotalent.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData;

public class LotController : ODataController
{
    private readonly LotService _lotService;
    private readonly IMapper _mapper;

    public LotController(LotService lotService, IMapper mapper)
    {
        _lotService = lotService;
        _mapper = mapper;
    }

    [EnableQuery]
    public IQueryable<LotDto> Get()
    {
        return _lotService
            .GetAll()
            .ProjectTo<LotDto>(_mapper.ConfigurationProvider);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<ActionResult<LotDto>> Get([FromODataUri] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotService.GetByIdAsync(key);
        return Ok(_mapper.Map<LotDto>(lot));
    }

    public async Task<ActionResult<LotDto>> Post([FromBody] LotDto purchaseOrderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = _mapper.Map<Lot>(purchaseOrderDto);
        await _lotService.AddAsync(lot);
        return Created("Lot", _mapper.Map<LotDto>(lot));
    }

    public async Task<ActionResult<LotDto>> Put([FromRoute] int key,
        [FromBody] LotDto lotDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotService.GetByIdAsync(key);
        if (lot == null)
        {
            return NotFound();
        }

        if (lot.Number != lotDto.Number)
        {
            return BadRequest("Unable to update vendor");
        }

        _mapper.Map(lotDto, lot);
        await _lotService.UpdateAsync(lot);
        return NoContent();
    }

    public async Task<ActionResult<LotDto>> Patch([FromRoute] int key,
        [FromBody] Delta<LotDto> lotDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotService.GetByIdAsync(key);
        if (lot == null)
        {
            return NotFound();
        }

        lotDto.TryGetPropertyValue("Number", out var numberProperty);
        if (numberProperty is string number && lot.Number != number)
        {
            return BadRequest("Unable to update order");
        }

        var dto = _mapper.Map<LotDto>(lot);
        lotDto.Patch(dto);

        var entity = _mapper.Map(dto, lot);

        await _lotService.UpdateAsync(entity);
        return Updated(_mapper.Map<LotDto>(entity));
    }

    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _lotService.DeleteByIdAsync(key);
        return NoContent();
    }
}
