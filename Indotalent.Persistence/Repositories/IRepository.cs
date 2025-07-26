using System.Linq.Expressions;

using Indotalent.Domain.Contracts;

namespace Indotalent.Persistence.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllArchive();
        IQueryable<T> GetAll();
        Task<T?> GetByIdAsync(int? id);
        IQueryable<T> GetByIdAsync(int? id, params Expression<Func<T, _Base?>>[] includes);
        Task<T> GetByRowGuidAsync(Guid? rowGuid);
        IQueryable<T> GetByRowGuidAsync(Guid? id, params Expression<Func<T, _Base?>>[] includes);
        Task AddAsync(T? entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T? entity);
        Task DeleteByIdAsync(int? id);
        Task DeleteByRowGuidAsync(Guid? rowGuid);
    }
}
