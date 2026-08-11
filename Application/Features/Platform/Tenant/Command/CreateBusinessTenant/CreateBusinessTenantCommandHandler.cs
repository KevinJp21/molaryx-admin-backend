using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Enums;
namespace Application.Features.Platform.Tenant.Command.CreateBusinessTenant
{
    public class CreateBusinessTenantCommandHandler
    (
        ITenantService _tenantService,
        IUserService _userService,
        ITenantSubscriptionService _tenantSubscriptionService,
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<CreateBusinessTenantCommand, bool>
    {

        public async Task<bool> Handle(CreateBusinessTenantCommand request, CancellationToken cancellationToken)
        {


            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var tenant = await _tenantService.CreatePendingTenantAsync(
                    (short)TenantTypeEnum.STANDARD,
                    request.Tenant,
                    cancellationToken
                );

                // Se guardan cambios porque el tenant debe existir para asignarlo a un user
                await _unitOfWork.SaveChangeAsync(cancellationToken);

                await _userService.CreatePendingOwnerAsync(
                    request.Owner,
                    tenant.IdTenant,
                    cancellationToken
                );

                await _tenantSubscriptionService.CreateCustomSubscriptionAsync(
                    tenant.IdTenant,
                    request.Plan.Price,
                    request.Plan.MaxProfessionals,
                    request.Plan.MaxAssistants,
                    request.Plan.MaxPatients,
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