using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Lots;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData;

public class LotItemController : ODataController
{
    private const string HeaderKeyName = "ParentId";
    private readonly LotItemService _lotItemService;
    private readonly IMapper _mapper;

    public LotItemController(LotItemService lotItemService, IMapper mapper)
    {
        _lotItemService = lotItemService;
        _mapper = mapper;
    }

    [EnableQuery]
    public IQueryable<LotItemDto> Get()
    {
        Request.Headers.TryGetValue(HeaderKeyName, out var headerValue);
        var parentId = int.Parse(headerValue.ToString());
        return _lotItemService
            .GetAll()
            .Where(x => x.LotId == parentId)
            .ProjectTo<LotItemDto>(_mapper.ConfigurationProvider);
    }

    [EnableQuery]
    [HttpGet("{key}")]
    public async Task<ActionResult<LotItemDto>> Get([FromODataUri] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotItemService.GetByIdAsync(key);
        return Ok(_mapper.Map<LotItemDto>(lot));
    }

    public async Task<ActionResult<LotItemDto>> Post([FromBody] LotItemDto lotItemDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Request.Headers.TryGetValue(HeaderKeyName, out var headerValue);
        var parentId = int.Parse(headerValue.ToString());

        lotItemDto.LotId = parentId;

        var lot = _mapper.Map<LotItem>(lotItemDto);
        await _lotItemService.AddAsync(lot);
        return Created("LotItem", _mapper.Map<LotItemDto>(lot));
    }

    public async Task<ActionResult<LotItemDto>> Put([FromRoute] int key,
        [FromBody] LotItemDto lotDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotItemService.GetByIdAsync(key);
        if (lot == null)
        {
            return NotFound();
        }

        _mapper.Map(lotDto, lot);
        await _lotItemService.UpdateAsync(lot);
        return NoContent();
    }

    public async Task<ActionResult<LotItemDto>> Patch([FromRoute] int key,
        [FromBody] Delta<LotItemDto> lotDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var lot = await _lotItemService.GetByIdAsync(key);
        if (lot == null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<LotItemDto>(lot);
        lotDto.Patch(dto);

        var entity = _mapper.Map(dto, lot);

        await _lotItemService.UpdateAsync(entity);
        return Updated(_mapper.Map<LotItemDto>(entity));
    }

    public async Task<ActionResult> Delete([FromRoute] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _lotItemService.DeleteByIdAsync(key);
        return NoContent();
    }
}
