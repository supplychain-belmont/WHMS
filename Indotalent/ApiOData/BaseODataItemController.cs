using System.Linq.Expressions;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Indotalent.Domain.Contracts;
using Indotalent.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Indotalent.ApiOData;

public class BaseODataItemController<T, TDto> : BaseODataController<T, TDto>
    where T : class, IHasId, IHasAudit, IHasSoftDelete
    where TDto : class
{
    protected const string HeaderKeyName = "ParentId";

    public BaseODataItemController(IRepository<T> service, IMapper mapper) : base(service, mapper)
    {
    }

    protected IQueryable<T> GetEntityWithIncludes(Expression<Func<T, bool>> condition, params Expression<Func<T, _Base?>>[] includes)
    {
        var query = _service.GetAll();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return query.Where(condition);
    }

    protected int SetParentId()
    {
        Request.Headers.TryGetValue(HeaderKeyName, out var headerValue);
        return int.Parse(headerValue.ToString());
    }
}
