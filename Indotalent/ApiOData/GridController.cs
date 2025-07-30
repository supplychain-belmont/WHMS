using AutoMapper;

using Indotalent.ApiOData;
using Indotalent.Domain.Grid;
using Indotalent.Templates.Dto;
using Indotalent.Templates.Service;

namespace Indotalent.Templates.Controller;


public class GridController : BaseODataController<Grid, GridDto>
{
    public GridController(GridService service, IMapper mapper) : base(service, mapper)
    {
    }
}
