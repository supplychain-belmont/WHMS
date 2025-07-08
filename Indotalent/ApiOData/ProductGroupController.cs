using AutoMapper;

using Indotalent.Applications.ProductGroups;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

namespace Indotalent.ApiOData
{
    public class ProductGroupController : BaseODataController<ProductGroup, ProductGroupDto>
    {
        public ProductGroupController(ProductGroupService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
