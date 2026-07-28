using Domain.Contracts.IRepositories;

namespace Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public ITenantRepository TenantRepository { get; }

        public ITenantStatusRepository TenantStatusRepository { get; }

        public IIdentificationTypeRepository IdentificationTypeRepository { get; }

        public IUserRepository UserRepository { get; }

        public IUserRoleRepository UserRoleRepository { get; }

        public IUserStatusRepository UserStatusRepository { get; }

        public IUserSessionRepository UserSessionRepository { get; }

        public IRoleHasPermissionRepository RoleHasPermissionRepository { get; }

        public ITenantSubscriptionRepository TenantSubscriptionRepository { get; }

        public IPlanRepository PlanRepository { get; }

        Task<int> SaveChangeAsync(CancellationToken? cancellationToken);

        Task BeginTransactionAsync(CancellationToken? cancellationToken = null);

        Task CommitTransactionAsync(CancellationToken? cancellationToken = null);

        Task RollbackTransactionAsync(CancellationToken? cancellationToken = null);

        bool IsInTransaction { get; }
    }
}