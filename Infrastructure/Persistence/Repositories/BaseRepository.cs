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

        public virtual async Task<TEntity?> GetByIdAsync(
            TKey id,
            CancellationToken cancellationToken = default,
            ISpecification<TEntity>? spec = null
           )
        {
            var keyName = Context.Model
                .FindEntityType(typeof(TEntity))
                ?.FindPrimaryKey()
                ?.Properties[0]
                .Name
                ?? throw new InvalidOperationException(
                    $"No se encontró la clave primaria de {typeof(TEntity).Name}.");

            var query = DbSet.Where(entity => EF.Property<TKey>(entity, keyName).Equals(id));

            query = query.Where(spec?.Criteria ?? (_ => true));

            foreach (var include in spec?.Includes ?? [])
            {
                query = query.Include(include);
            }

            foreach (var includePath in spec?.IncludePaths ?? [])
            {
                query = query.Include(includePath);
            }

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbSet.Update(entity);

            return Task.CompletedTask;
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
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
            var query = ApplyIncludes(
                ApplyCriteria(DbSet.AsNoTracking(), spec),
                spec);

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return (totalItems, items);
        }

        public virtual Task<bool> ExistsAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default)
        {
            return ApplyCriteria(DbSet, spec).AnyAsync(cancellationToken);
        }

        public virtual Task<int> CountAsync(
            ISpecification<TEntity>? spec = null,
            CancellationToken cancellationToken = default)
        {
            return ApplyCriteria(DbSet, spec).CountAsync(cancellationToken);
        }

        private static IQueryable<TEntity> ApplyCriteria(
            IQueryable<TEntity> query,
            ISpecification<TEntity>? spec)
        {
            return query.Where(spec?.Criteria ?? (_ => true));
        }

        private static IQueryable<TEntity> ApplyIncludes(
            IQueryable<TEntity> query,
            ISpecification<TEntity>? spec)
        {
            foreach (var include in spec?.Includes ?? [])
            {
                query = query.Include(include);
            }

            foreach (var includePath in spec?.IncludePaths ?? [])
            {
                query = query.Include(includePath);
            }

            return query;
        }
    }
}