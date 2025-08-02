using Indotalent.Domain.Enums;

using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData;

public class StatusController : ODataController
{
    [EnableQuery]
    public IQueryable<StatusDto> Get()
    {
        var names = Enum.GetValues(typeof(Status))
            .Cast<Status>()
            .Select(status => new StatusDto { Name = status.ToString(), Value = status.GetDescription() });
        return names.AsQueryable();
    }
}

public class StatusDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Value { get; set; }
}
