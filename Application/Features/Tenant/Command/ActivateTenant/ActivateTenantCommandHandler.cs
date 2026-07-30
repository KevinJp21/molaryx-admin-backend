using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;

namespace Application.Features.Tenant.Command.ActivateTenant
{
    public class ActivateTenantCommandHandler(
        ITenantService _tenantService,
        IUserService _userService,
        ITenantSubscriptionService _tenantSubscriptionService,
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<ActivateTenantCommand, bool>
    {

        public async Task<bool> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {

                await _userService.ActivateUserAsync(
                    request.IdUser,
                    cancellationToken
                );

                await _tenantService.ActivateTenantAsync(
                    request.IdTenant,
                    cancellationToken
                );

                await _tenantSubscriptionService.ActivateSubscriptionAsync(
                    request.IdTenantSubscription,
                    cancellationToken
                );

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return true;
            }
            catch
            {
                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                }
                throw;
            }
        }

    }
}