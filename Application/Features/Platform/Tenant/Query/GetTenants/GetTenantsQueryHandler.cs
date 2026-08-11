using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.DTOs.Tenant;
using Domain.Contracts;
using Domain.Enums;
using Domain.Specifications;


namespace Application.Features.Platform.Tenant.Query.GetTenants
{
    public class GetTenantsQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetTenantsQuery, PagedResult<TenantDto>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<PagedResult<TenantDto>> Handle(
            GetTenantsQuery request,
            CancellationToken cancellationToken = default)
        {
            var page = PaginationHelper.GetEffectivePage(request.Page);
            var size = PaginationHelper.GetEffectivePageSize(request.Size);

            var spec = new TenantsSpec();

            var (totalItems, tenants) = await _unitOfWork.TenantRepository.GetPagedAsync(
                page,
                size,
                spec: spec,
                cancellationToken: cancellationToken
            );

            return new PagedResult<TenantDto>
            {
                Items = [
                    .. tenants.Select(t => new TenantDto
                    {
                        IdTenant = t.IdTenant,
                        IdTenantSubscription = t.TenantSubscriptions
                            .OrderByDescending(ts => ts.CreatedAt)
                            .Select(ts => (long?)ts.IdTenantSubscription)
                            .FirstOrDefault(),
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
                        .Select(u => new OwnerDto{
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
                        }).FirstOrDefault()
                    })
                ],
                Page = page,
                Size = size,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)size)
            };
        }
    }
}