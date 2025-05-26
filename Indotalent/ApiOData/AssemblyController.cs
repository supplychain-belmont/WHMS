using AutoMapper;

using Indotalent.Applications.Products;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

namespace Indotalent.ApiOData
{
    public class AssemblyController : BaseODataController<Assembly, AssemblyDto>
    {
        public AssemblyController(AssemblyService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
