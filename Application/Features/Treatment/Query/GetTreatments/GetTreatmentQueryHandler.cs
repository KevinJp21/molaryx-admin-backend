using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Treatment.Query.GetTreatments
{
    public class GetTreatmentQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetTreatmentsQuery, PagedResult<GetTreatmentsResponse>>
    {
        public async Task<PagedResult<GetTreatmentsResponse>> Handle(GetTreatmentsQuery request, CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new TreatmentsSpec(access.IdTenant, request.IsActive);

            var (totalItems, treatments) = await _unitOfWork.TreatmentRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetTreatmentsResponse>
            {
                Items = [.. treatments.Select(service => new
                    GetTreatmentsResponse
                    {
                        IdTreatment = service.IdTreatment,
                        Name = service.Name,
                        Description = service.Description!,
                        IsActive = service.IsActive
                    })
                ],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size))
            };
        }
    }
}
