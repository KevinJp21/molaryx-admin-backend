using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
namespace Application.Features.RegisterTenant.Command
{
    public class RegisterTenantCommandHandler
    (
        ITenantService tenantService,
        IUserService userService,
        ITenantSubscriptionService tenantSubscriptionService,
        IUnitOfWork unitOfWork
    ) : IRequestHandler<RegisterTenantCommand, bool>
    {

        private readonly ITenantService _tenantService = tenantService;
        private readonly IUserService _userService = userService;
        private readonly ITenantSubscriptionService _tenantSubscriptionService = tenantSubscriptionService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(
            RegisterTenantCommand request,
            CancellationToken cancellationToken)
        {

            await _unitOfWork.BeginTransactionAsync( cancellationToken );

            try
            {
                var tenant = await _tenantService.CreatePendingTenantAsync(
                    request.Tenant,
                    cancellationToken
                );

                await _unitOfWork.SaveChangeAsync(cancellationToken);

                await _userService.CreatePendingOwnerAsync(
                    request.Owner,
                    tenant.IdTenant,
                    cancellationToken
                );

                await _tenantSubscriptionService.CreateSubscriptionAsync(
                    tenant.IdTenant,
                    request.IdPlan,
                    cancellationToken
                );

                await _unitOfWork.CommitTransactionAsync( cancellationToken );

                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }
    }
}