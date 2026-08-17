using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts.IServices;
using Domain.Contracts;
using Domain.Specifications;

namespace Application.Features.Service.Query
{
    public class GetServicesQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetServicesQuery, PagedResult<GetServicesResponse>>
    {
        public async Task<PagedResult<GetServicesResponse>> Handle(GetServicesQuery request, CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new ServicesSpec(access.IdTenant, request.IsActive, request.Search);

            var (totalItems, services) = await _unitOfWork.ServiceRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetServicesResponse>
            {
                Items = [.. services.Select(service => new GetServicesResponse
                {
                    IdService = service.IdService,
                    Name = service.Name,
                    Description = service.Description!,
                    IsActive = service.IsActive,
                })
                ],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }
    }
}
