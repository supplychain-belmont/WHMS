using System.Linq.Expressions;
using System.Security.Claims;

using AutoMapper.QueryableExtensions;

using Indotalent.Data;
using Indotalent.Domain.Contracts;

using Microsoft.EntityFrameworkCore;

namespace Indotalent.Infrastructures.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IHasId, IHasAudit, IHasSoftDelete
    {
        protected readonly ApplicationDbContext _context;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IAuditColumnTransformer _auditColumnTransformer;
        protected readonly string _userId;
        protected readonly string _userName;
        private const string NameSpace = "https://belmont.com";

        public Repository(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IAuditColumnTransformer auditColumnTransformer)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _auditColumnTransformer = auditColumnTransformer;
            _userId = GetUserId(_httpContextAccessor);
            _userName = GetUserName(_httpContextAccessor);
        }

        private static string GetUserId(IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private static string GetUserName(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user?.FindFirst($"{NameSpace}/name")?.Value
                   ?? user?.FindFirst($"{NameSpace}/email")?.Value
                   ?? string.Empty;
        }

        public virtual IQueryable<T> GetAllArchive()
        {
            return _context.Set<T>()
                .ApplyIsDeletedFilter()
                .AsNoTracking();
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>()
                .ApplyIsNotDeletedFilter()
                .AsNoTracking();
        }

        public virtual async Task<T?> GetByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Unable to process, id is null");
            }

            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                await _auditColumnTransformer.TransformAsync(entity, _context);
            }

            return entity;
        }

        public virtual IQueryable<T> GetByIdAsync(int? id, params Expression<Func<T, _Base?>>[] includes)
        {
            if (!id.HasValue)
            {
                throw new Exception("Unable to process, id is null");
            }

            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));
            return query.Where(x => x.Id == id);
        }

        public virtual IQueryable<T> GetByRowGuidAsync(Guid? rowGuid,
            params Expression<Func<T, _Base?>>[] includes)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, rowGuid is null");
            }

            IQueryable<T> query = _context.Set<T>();
            query = includes.Aggregate(query,
                (current, include) => current.Include(include));
            return query.Where(x => x.RowGuid == rowGuid);
        }

        public virtual async Task<T?> GetByRowGuidAsync(Guid? rowGuid)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, row guid is null");
            }

            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.RowGuid == rowGuid);

            if (entity != null)
            {
                await _auditColumnTransformer.TransformAsync(entity, _context);
            }

            return entity;
        }

        public virtual async Task AddAsync(T? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.CreatedAtUtc = DateTime.Now;
                    auditEntity.CreatedByUserId = _userId;
                    auditEntity.CreatedByUserName = _userName;
                }

                entity.RowGuid = Guid.NewGuid();

                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            if (entities.Count != 0)
            {
                foreach (var entity in entities)
                {
                    if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                    {
                        auditEntity.CreatedAtUtc = DateTime.Now;
                        auditEntity.CreatedByUserId = _userId;
                        auditEntity.CreatedByUserName = _userName;
                    }

                    entity.RowGuid = Guid.NewGuid();
                }

                await _context.Set<T>().AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unable to process, entities are null or empty");
            }
        }

        public virtual async Task UpdateAsync(T? entity)
        {
            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                    auditEntity.UpdatedByUserName = _userName;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Unable to process, entity is null");
            }
        }

        public virtual async Task DeleteByIdAsync(int? id)
        {
            if (!id.HasValue)
            {
                throw new Exception("Unable to process, id is null");
            }

            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                    auditEntity.UpdatedByUserName = _userName;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                if (entity is IHasSoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsNotDeleted = false;
                    _context.SetModifiedState(entity);
                }
                else
                {
                    _context.Set<T>().Remove(entity);
                }

                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task DeleteByRowGuidAsync(Guid? rowGuid)
        {
            if (!rowGuid.HasValue)
            {
                throw new Exception("Unable to process, row guid is null");
            }

            var entity = await _context.Set<T>()
                .FirstOrDefaultAsync(x => x.RowGuid == rowGuid);

            if (entity != null)
            {
                if (entity is IHasAudit auditEntity && !string.IsNullOrEmpty(_userId))
                {
                    auditEntity.UpdatedByUserId = _userId;
                    auditEntity.UpdatedByUserName = _userName;
                }

                if (entity is IHasAudit auditedEntity)
                {
                    auditedEntity.UpdatedAtUtc = DateTime.Now;
                }

                if (entity is IHasSoftDelete softDeleteEntity)
                {
                    softDeleteEntity.IsNotDeleted = false;
                    _context.SetModifiedState(entity);
                }
                else
                {
                    _context.Set<T>().Remove(entity);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
