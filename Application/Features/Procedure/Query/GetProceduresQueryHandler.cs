using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Procedure.Query
{
    public class GetProceduresQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetProceduresQuery, PagedResult<GetProceduresResponse>>
    {
        public async Task<PagedResult<GetProceduresResponse>> Handle(
            GetProceduresQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new ProceduresSpec(access.IdTenant, request.IsActive, request.Search);

            var (totalItems, procedures) = await _unitOfWork.ProcedureRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetProceduresResponse>
            {
                Items = [.. procedures.Select(procedure => new GetProceduresResponse
                {
                    IdProcedure = procedure.IdProcedure,
                    Name = procedure.Name,
                    Description = procedure.Description!,
                    ReferencePrice = procedure.ReferencePrice,
                    IsActive = procedure.IsActive,
                })],
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
