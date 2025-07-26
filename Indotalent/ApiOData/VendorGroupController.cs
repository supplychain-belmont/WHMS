using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.VendorGroups;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class VendorGroupController : BaseODataController<VendorGroup, VendorGroupDto>
    {
        public VendorGroupController(VendorGroupService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
