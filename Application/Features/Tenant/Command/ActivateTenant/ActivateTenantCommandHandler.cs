using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;

namespace Application.Features.Tenant.Command.ActivateTenant
{
    public class ActivateTenantCommandHandler(
        ITenantService _tenantService,
        IUserService _userService,
        ITenantSubscriptionService _tenantSubscriptionService,
        IEmailNotificationService _emailNotificationService,
        IUnitOfWork _unitOfWork,
        ILogger<ActivateTenantCommandHandler> _logger
    ) : IRequestHandler<ActivateTenantCommand, bool>
    {

        public async Task<bool> Handle(ActivateTenantCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {

                var user = await _userService.ActivateUserAsync(
                    request.IdUser,
                    cancellationToken
                );

                var tenant = await _tenantService.ActivateTenantAsync(
                    request.IdTenant,
                    cancellationToken
                );

                await _tenantSubscriptionService.ActivateSubscriptionAsync(
                    request.IdTenantSubscription,
                    cancellationToken
                );

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                try
                {
                    await _emailNotificationService.SendAccountActivatedEmailAsync(
                        user,
                        tenant,
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(
                        ex,
                        "Error al enviar el correo de activación de cuenta al usuario {UserEmail}.",
                        user.Email
                    );
                }

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