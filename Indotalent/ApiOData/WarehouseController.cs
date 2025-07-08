using AutoMapper;

using Indotalent.Applications.Warehouses;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

namespace Indotalent.ApiOData
{
    public class WarehouseController : BaseODataController<Warehouse, WarehouseDto>
    {
        public WarehouseController(WarehouseService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
