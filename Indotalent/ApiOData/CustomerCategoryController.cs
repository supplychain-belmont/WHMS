using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Applications.CustomerCategories;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Indotalent.ApiOData
{
    public class CustomerCategoryController : BaseODataController<CustomerCategory, CustomerCategoryDto>
    {
        public CustomerCategoryController(CustomerCategoryService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
