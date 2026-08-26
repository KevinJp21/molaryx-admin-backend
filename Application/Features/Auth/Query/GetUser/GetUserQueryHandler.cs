using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts;
using Domain.Specifications;
using Shared.Utils;

namespace Application.Features.Auth.Query.GetUser
{
    public class GetUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUser currentUser) : IRequestHandler<GetUserQuery, GetUserQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<GetUserQueryResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {

            var spec = new UserSpec((long)_currentUser.IdUser!);

            var user = await _unitOfWork.UserRepository.GetByIdAsync((long)_currentUser.IdUser!, cancellationToken, spec)
                ?? throw new Exception("Usuario no encontrado.");

            var permissions = await _unitOfWork.UserRepository.GetPermissionsByUserIdAsync((long)_currentUser.IdUser!, cancellationToken);

            var mapperResult = new GetUserQueryResponse
            {
                Role = new UserRole
                {
                    IdUserRole = user.UserRole.IdUserRole,
                    Name = user.UserRole.Name
                },
                IdTenant = user.IdTenant,
                Status = new UserStatus
                {
                    IdUserStatus = user.UserStatus.IdUserStatus,
                    Name = user.UserStatus.Name
                },
                Username = user.Username,
                Names = $"{user.FirstName}{(!string.IsNullOrEmpty(user.SecondName) ? $" {user.SecondName}" : string.Empty)}",
                Surnames = $"{user.FirstSurname}{(!string.IsNullOrEmpty(user.SecondSurname) ? $" {user.SecondSurname}" : string.Empty)}",
                Email = user.Email,
                Subscription = await ResolveSubscriptionAsync(user.IdTenant, cancellationToken),
                Permissions = [.. permissions
                    .GroupBy(p => p.Module.Code)
                    .Select(g => new ModulePermissions
                    {
                        Module = g.Key,
                        Codes = [.. g.Select(p => p.Code)]
                    })]
            };

            return mapperResult;
        }

        private async Task<SubscriptionSummary?> ResolveSubscriptionAsync(
            long? idTenant,
            CancellationToken cancellationToken)
        {
            if (idTenant is not long tenantId)
            {
                return null;
            }

            var subscription = await _unitOfWork.TenantSubscriptionRepository.GetFirstAsync(
                TenantSubscriptionSpec.ActiveByTenant(tenantId),
                cancellationToken)
                ?? await _unitOfWork.TenantSubscriptionRepository.GetFirstAsync(
                    TenantSubscriptionSpec.LatestByTenant(tenantId),
                    cancellationToken);

            if (subscription is null)
            {
                return null;
            }

            return new SubscriptionSummary
            {
                PlanName = subscription.Plan?.Name ?? string.Empty,
                StartsAt = subscription.StartsAt,
                EndsAt = subscription.EndsAt,
                DaysRemaining = CalculateDaysRemaining(subscription.EndsAt),
                StatusName = subscription.TenantSubscriptionStatus?.Name ?? string.Empty
            };
        }

        private static int? CalculateDaysRemaining(DateTime? endsAt)
        {
            if (!endsAt.HasValue)
            {
                return null;
            }

            var today = DateTimeHelper.ToColombiaTime(DateTime.UtcNow).Date;
            var endDate = DateTimeHelper.ToColombiaTime(endsAt.Value).Date;
            return (endDate - today).Days;
        }
    }
}
