using Indotalent.Domain.Enums;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData;

public class StatusController : ODataController
{
    [EnableQuery]
    public IQueryable<StatusDto> Get()
    {
        var names = Enum.GetNames(typeof(Status))
            .Select(value => new StatusDto { Name = value, Value = value });
        return names.AsQueryable();
    }
}

public class StatusDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
}
