using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
namespace Application.Features.Tenant.Command.RegisterTenant
{
    public class RegisterTenantCommandHandler
    (
        ITenantService _tenantService,
        IUserService _userService,
        ITenantSubscriptionService _tenantSubscriptionService,
        IPromotionService _promotionService,
        IUnitOfWork _unitOfWork
    ) : IRequestHandler<RegisterTenantCommand, bool>
    {

        public async Task<bool> Handle(RegisterTenantCommand request, CancellationToken cancellationToken)
        {

            Promotion? promotion = null;

            if (request.IdPromotion is long idPromotion)
            {
                 promotion = await _promotionService.ValidatePromotionAsync(idPromotion, request.IdPlan, cancellationToken);
            }


            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var tenant = await _tenantService.CreatePendingTenantAsync(
                    promotion?.IdTenantType ?? (short)TenantTypeEnum.STANDARD,
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

                await _tenantSubscriptionService.CreateSubscriptionAsync(
                    tenant.IdTenant,
                    request.IdPlan,
                    request.IdPromotion,
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