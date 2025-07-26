using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Domain.Contracts;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData;

public abstract class BaseODataController<T, TDto> : ODataController
    where T : class, IHasId, IHasAudit, IHasSoftDelete
    where TDto : class
{
    protected readonly IRepository<T> _service;
    protected readonly IMapper _mapper;

    protected BaseODataController(IRepository<T> service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [EnableQuery]
    public virtual IQueryable<TDto> Get()
    {
        return _service
            .GetAll()
            .ProjectTo<TDto>(_mapper.ConfigurationProvider);
    }

    [EnableQuery]
    public virtual async Task<ActionResult<TDto>> Get([FromODataUri] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _service.GetByIdAsync(key);
        return Ok(_mapper.Map<TDto>(entity));
    }

    public virtual async Task<ActionResult<TDto>> Post([FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = _mapper.Map<T>(dto);
        await _service.AddAsync(entity);
        return Created(_mapper.Map<TDto>(entity));
    }

    public virtual async Task<ActionResult<TDto>> Put([FromODataUri] int key, [FromBody] TDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _service.GetByIdAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        _mapper.Map(dto, entity);
        await _service.UpdateAsync(entity);
        return Updated(_mapper.Map<TDto>(entity));
    }


    public async Task<ActionResult<TDto>> Patch([FromRoute] int key,
        [FromBody] Delta<TDto> purchaseOrderDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var order = await _service.GetByIdAsync(key);
        if (order == null)
        {
            return NotFound();
        }

        try
        {
            var dto = _mapper.Map<TDto>(order);
            purchaseOrderDto.Patch(dto);
            _mapper.Map(dto, order);
            await _service.UpdateAsync(order);
            return Updated(_mapper.Map<TDto>(order));
        }
        catch (Exception e)
        {
            return UnprocessableEntity(e.Message);
        }
    }


    public virtual async Task<ActionResult> Delete([FromODataUri] int key)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _service.GetByIdAsync(key);
        if (entity == null)
        {
            return NotFound();
        }

        await _service.DeleteByIdAsync(key);
        return NoContent();
    }

    protected async Task<ActionResult<TDto>> GetEntityWithIncludesAsync(int key,
        params Expression<Func<T, _Base?>>[] includes)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await _service.GetByIdAsync(key, includes)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        if (entity == null)
        {
            return NotFound();
        }

        return Ok(entity);
    }
}
