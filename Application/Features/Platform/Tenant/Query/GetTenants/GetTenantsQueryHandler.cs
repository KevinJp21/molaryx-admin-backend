using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Enums;
using Domain.Specifications;
using Shared.Utils;

namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetTenantsQuery, PagedResult<GetTenantsResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PagedResult<GetTenantsResponse>> Handle(
            GetTenantsQuery request,
            CancellationToken cancellationToken = default)
        {
            var page = PaginationHelper.GetEffectivePage(request.Page);
            var size = PaginationHelper.GetEffectivePageSize(request.Size);

            var spec = new TenantsSpec(request.IdTenantStatus, request.Search);

            var (totalItems, tenants) = await _unitOfWork.TenantRepository.GetPagedAsync(
                page,
                size,
                spec: spec,
                cancellationToken: cancellationToken
            );

            return new PagedResult<GetTenantsResponse>
            {
                Items =
                [
                    .. tenants.Select(t => new GetTenantsResponse
                    {
                        IdTenant = t.IdTenant,
                        IdIdentificationType = t.IdIdentificationType,
                        IdentificationNumber = t.IdentificationNumber,
                        IdentificationCode = t.IdentificationType.Code,
                        ConsultoryName = t.ConsultoryName,
                        Email = t.Email,
                        PhoneNumber = t.PhoneNumber,
                        Address = t.Address,
                        IdTenantType = t.TenantType.IdTenantType,
                        TenantTypeCode = t.TenantType.Code,
                        IdTenantStatus = t.TenantStatus.IdTenantStatus,
                        TenantStatusName = t.TenantStatus.Name,
                        Owner = t.Users.Where(u => u.IdUserRole == (short)UserRoleEnum.OWNER)
                            .Select(u => new GetTenantsOwnerResponse
                            {
                                IdUser = u.IdUser,
                                Username = u.Username,
                                Name = string.Join(
                                    " ",
                                    new[]
                                    {
                                        u.FirstName,
                                        u.SecondName,
                                        u.FirstSurname,
                                        u.SecondSurname
                                    }.Where(x => !string.IsNullOrWhiteSpace(x))
                                ),
                                IdIdentificationType = u.IdIdentificationType,
                                IdentificationCode = u.IdentificationType.Code,
                                IdentificationNumber = u.IdentificationNumber,
                                PhoneNumber = u.PhoneNumber,
                                Email = u.Email
                            }).FirstOrDefault(),
                        Subscription = MapSubscription(t)
                    })
                ],
                Page = page,
                Size = size,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }

        private static GetTenantsSubscriptionResponse? MapSubscription(Domain.Entities.Tenant tenant)
        {
            var subscription = tenant.TenantSubscriptions
                .Where(ts =>
                    ts.IdTenantSubscriptionStatus == (short)TenantSubscriptionStatusEnum.ACTIVE)
                .OrderByDescending(ts => ts.StartsAt)
                .FirstOrDefault()
                ?? tenant.TenantSubscriptions
                    .OrderByDescending(ts => ts.StartsAt ?? ts.CreatedAt)
                    .FirstOrDefault();

            if (subscription is null)
            {
                return null;
            }

            return new GetTenantsSubscriptionResponse
            {
                IdTenantSubscription = subscription.IdTenantSubscription,
                IdTenant = subscription.IdTenant,
                IdTenantSubscriptionStatus = subscription.IdTenantSubscriptionStatus,
                StatusName = subscription.TenantSubscriptionStatus?.Name ?? string.Empty,
                IdPlan = subscription.IdPlan,
                PlanName = subscription.Plan?.Name ?? string.Empty,
                Price = subscription.Price,
                MaxProfessionals = subscription.MaxProfessionals,
                MaxAssistants = subscription.MaxAssistants,
                MaxPatients = subscription.MaxPatients,
                StartsAt = subscription.StartsAt,
                EndsAt = subscription.EndsAt,
                DaysRemaining = CalculateDaysRemaining(subscription.EndsAt),
                PromotionEndsAt = subscription.PromotionEndsAt,
                IsPromotionActive = subscription.IsPromotionActive
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
