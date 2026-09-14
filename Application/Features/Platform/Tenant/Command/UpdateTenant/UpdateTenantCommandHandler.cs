using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;

namespace Application.Features.Platform.Tenant.Command.UpdateTenant
{
    public class UpdateTenantCommandHandler(
        ITenantService _tenantService,
        IUserService _userService,
        ITenantSubscriptionService _tenantSubscriptionService,
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<UpdateTenantCommand, bool>
    {
        public async Task<bool> Handle(
            UpdateTenantCommand request,
            CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                if (request.Tenant is not null)
                {
                    await _tenantService.UpdateTenantAsync(
                        request.IdTenant,
                        request.Tenant,
                        cancellationToken);
                }

                if (request.Owner is not null)
                {
                    await _userService.UpdateOwnerAsync(
                        request.IdTenant,
                        request.Owner,
                        cancellationToken);
                }

                if (request.Subscription is not null)
                {
                    await _tenantSubscriptionService.UpdateSubscriptionAsync(
                        request.IdTenant,
                        request.Subscription,
                        cancellationToken);
                }

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
