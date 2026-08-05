using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class where TKey : notnull
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        public BaseRepository(AppDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual async Task<TEntity[]?> GetAll(
            ISpecification<TEntity>? spec = null,
            CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = DbSet;

            query = query.Where(spec?.Criteria ?? (_ => true));

            foreach (var include in spec?.Includes ?? [])
            {
                query = query.Include(include);
            }

            foreach (var includePath in spec?.IncludePaths ?? [])
            {
                query = query.Include(includePath);
            }

            return await query.ToArrayAsync(cancellationToken);
        }

        public virtual async Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            var result = await DbSet.AddAsync(entity, cancellationToken);
            return result.State == EntityState.Added;
        }

        public virtual async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync([id], cancellationToken);
        }

        public virtual Task UpdateAsync( TEntity entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);

            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync( CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(
                cancellationToken
            );
        }

        public virtual void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual async Task<(int totalItems, List<TEntity> data)> GetPagedAsync(
            int page,
            int size,
            ISpecification<TEntity>? spec = null,
            CancellationToken cancellationToken = default)
        {
            var query = DbSet
                .AsNoTracking()
                .Where(spec?.Criteria ?? (_ => true));

            foreach (var include in spec?.Includes ?? [])
            {
                query = query.Include(include);
            }

            foreach (var path in spec?.IncludePaths ?? [])
            {
                query = query.Include(path);
            }

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (totalItems, items);
        }
    }
}