using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.Products;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

using Microsoft.AspNetCore.Mvc;

namespace Indotalent.ApiOData;

public class AssemblyChildController : BaseODataItemController<AssemblyChild, AssemblyChildDto>
{
    public AssemblyChildController(AssemblyChildService service, IMapper mapper) : base(service, mapper)
    {
    }

    public override IQueryable<AssemblyChildDto> Get()
    {
        var parentId = SetParentId();
        return _service
            .GetAll()
            .Where(x => x.AssemblyId == parentId)
            .ProjectTo<AssemblyChildDto>(_mapper.ConfigurationProvider);
    }

    public override async Task<ActionResult<AssemblyChildDto>> Post(AssemblyChildDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var parentId = SetParentId();
        dto.AssemblyId = parentId;
        return await base.Post(dto);
    }
}
