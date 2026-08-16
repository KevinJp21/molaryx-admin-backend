using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Users.Query.GetProfessionals
{
    public class GetProfessionalsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetProfessionalsQuery, PagedResult<GetProfessionalsResponse>>
    {
        public async Task<PagedResult<GetProfessionalsResponse>> Handle(
            GetProfessionalsQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new ProfessionalSpec(access.IdTenant, request.IdUserStatus);

            var (totalItems, professionals) = await _unitOfWork.ProfessionalRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetProfessionalsResponse>
            {
                Items = [.. professionals.Select(professional => new GetProfessionalsResponse
                {
                    IdProfessional = professional.IdProfessional,
                    IdUserStatus = professional.User.IdUserStatus,
                    StatusName = professional.User.UserStatus.Name,
                    Username = professional.User.Username,
                    FirstName = professional.User.FirstName,
                    SecondName = professional.User.SecondName,
                    FirstSurname = professional.User.FirstSurname,
                    SecondSurname = professional.User.SecondSurname,
                    IdentificationType = professional.User.IdentificationType.Name,
                    IdentificationNumber = professional.User.IdentificationNumber,
                    BirthDate = professional.User.BirthDate.ToString(),
                    PhoneNumber = professional.User.PhoneNumber,
                    Email = professional.User.Email
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
