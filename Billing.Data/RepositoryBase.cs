using Billing.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Billing.Data
{
    public abstract class RepositoryBase<TEntity, U> : IDataRepository<TEntity>
        where TEntity : class, new()
        where U : DbContext
    {
        protected readonly U _Context;
        protected readonly DbSet<TEntity> _DbSet;

        protected RepositoryBase(U context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        public virtual async ValueTask<TEntity> AddAsync(TEntity entity)
        {
            await _Context.Set<TEntity>().AddAsync(entity);

            await _Context.SaveChangesAsync();

            return entity;
        }

        public virtual async ValueTask<TEntity> UpdateAsync(TEntity entity)
        {
            _DbSet.Attach(entity);
            _Context.Entry<TEntity>(entity).State = EntityState.Modified;

            await _Context.SaveChangesAsync();

            return entity;
        }

        public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            return await notSortedResults.ToListAsync();
        }

        public virtual async ValueTask<IEnumerable<TResult>> GetAllAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            return await notSortedResults.ToListAsync();
        }

        public virtual async ValueTask<TResult> GetAsync<TResult>(Func<IQueryable<TEntity>, IQueryable<TResult>> transform, Expression<Func<TEntity, bool>> filter = null)
        {
            var query = filter == null ? _DbSet.AsNoTracking() : _DbSet.AsNoTracking().Where(filter);

            var notSortedResults = transform(query);

            var result = await notSortedResults.FirstOrDefaultAsync();

            return result;
        }
    }
}