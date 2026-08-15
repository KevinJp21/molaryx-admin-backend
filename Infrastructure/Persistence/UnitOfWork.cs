using Domain.Contracts;
using Domain.Contracts.IRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    public class UnitOfWork(
        AppDbContext context,
        ITenantRepository tenantRepository,
        ITenantStatusRepository tenantStatusRepository,
        IIdentificationTypeRepository identificationTypeRepository,
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IUserStatusRepository userStatusRepository,
        IUserSessionRepository userSessionRepository,
        IRolePermissionRepository RolePermissionRepository,
        ITenantSubscriptionRepository tenantSubscriptionRepository,
        IPlanRepository planRepository,
        IPromotionRepository promotionRepository,
        IPasswordResetTokenRepository passwordResetTokenRepository,
        IPatientsRepository patientsRepository,
        IServiceRepository serviceRepository,
        IAppointmentRepository appointmentRepository,
        IProfessionalRepository professionalRepository
        ) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        public ITenantRepository TenantRepository { get; } = tenantRepository;

        public ITenantStatusRepository TenantStatusRepository { get; } = tenantStatusRepository;

        public IIdentificationTypeRepository IdentificationTypeRepository { get; } = identificationTypeRepository;

        public IUserRepository UserRepository { get; } = userRepository;

        public IUserRoleRepository UserRoleRepository { get; } = userRoleRepository;

        public IUserStatusRepository UserStatusRepository { get; } = userStatusRepository;

        public IUserSessionRepository UserSessionRepository { get; } = userSessionRepository;

        public IRolePermissionRepository RolePermissionRepository { get; } = RolePermissionRepository;

        public ITenantSubscriptionRepository TenantSubscriptionRepository { get; } = tenantSubscriptionRepository;

        public IPlanRepository PlanRepository { get; } = planRepository;

        public IPromotionRepository PromotionRepository { get; } = promotionRepository;

        public IPasswordResetTokenRepository PasswordResetTokenRepository { get; } = passwordResetTokenRepository;

        public IPatientsRepository PatientsRepository { get; } = patientsRepository;

        public IServiceRepository ServiceRepository { get; } = serviceRepository;

        public IAppointmentRepository AppointmentRepository { get; } = appointmentRepository;

        public IProfessionalRepository ProfessionalRepository { get; } = professionalRepository;

        private IDbContextTransaction? _currentTransaction;

        public bool IsInTransaction => _currentTransaction != null;

        public async Task BeginTransactionAsync(
            CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction != null)
                return;

            _currentTransaction = await _context.Database
                .BeginTransactionAsync(cancellationToken ?? CancellationToken.None);
        }

        public async Task CommitTransactionAsync(CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction == null)
                return;

            try
            {
                await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
                await _currentTransaction.CommitAsync(cancellationToken ?? CancellationToken.None);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken? cancellationToken = null)
        {
            if (_currentTransaction == null)
                return;

            await _currentTransaction.RollbackAsync(cancellationToken ?? CancellationToken.None);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }

        public async Task<int> SaveChangeAsync(CancellationToken? cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }

        public async ValueTask DisposeAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }

            await _context.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
