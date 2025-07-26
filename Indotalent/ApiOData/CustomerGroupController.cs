using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.CustomerGroups;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class CustomerGroupController : BaseODataController<CustomerGroup, CustomerGroupDto>
    {
        public CustomerGroupController(CustomerGroupService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
