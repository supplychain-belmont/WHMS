using AutoMapper;

using Indotalent.Applications.UnitMeasures;
using Indotalent.Domain.Entities;
using Indotalent.DTOs;

namespace Indotalent.ApiOData
{
    public class UnitMeasureController : BaseODataController<UnitMeasure, UnitMeasureDto>
    {
        public UnitMeasureController(UnitMeasureService service, IMapper mapper) : base(service, mapper)
        {
        }
    }
}
