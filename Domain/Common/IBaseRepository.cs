namespace Domain.Common
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class where TKey : notnull
    {
        Task<TEntity[]?> GetAll(ISpecification<TEntity>? spec = null, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(
            TKey id,
            CancellationToken cancellationToken = default,
            ISpecification<TEntity>? spec = null
        );
        Task<bool> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken);
        Task SaveChangesAsync( CancellationToken cancellationToken );
        void Remove(TEntity entity);
        Task<(int totalItems, List<TEntity> data)> GetPagedAsync(
            int page,
            int size,
            ISpecification<TEntity>? spec = null,
            CancellationToken cancellationToken = default
        );

        Task<bool> ExistsAsync(
            ISpecification<TEntity> spec,
            CancellationToken cancellationToken = default
        );

        Task<int> CountAsync(
            ISpecification<TEntity>? spec = null,
            CancellationToken cancellationToken = default
        );
    }
}