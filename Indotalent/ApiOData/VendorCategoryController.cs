using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.VendorCategories;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Infrastructures.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class VendorCategoryController : BaseODataController<VendorCategory, VendorCategoryDto>
    {
        public VendorCategoryController(VendorCategoryService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
