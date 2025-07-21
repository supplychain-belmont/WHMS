using AutoMapper;

using Indotalent.ApiOData;
using Indotalent.Data;
using Indotalent.Domain.Contracts;
using Indotalent.Infrastructures.Repositories;

namespace Indotalent.Templates.Controller;

#if (!EnableClasses)

#region Classes for compilation purposes

public abstract class _BaseDto
{
}

public class _BaseService : Repository<_Base>
{
    public _BaseService(
        ApplicationDbContext context,
        IHttpContextAccessor httpContextAccessor,
        IAuditColumnTransformer auditColumnTransformer) :
        base(
            context,
            httpContextAccessor,
            auditColumnTransformer)
    {
    }
}

#endregion

#endif

public class _BaseController : BaseODataController<_Base, _BaseDto>
{
    public _BaseController(_BaseService service, IMapper mapper) : base(service, mapper)
    {
    }
}
