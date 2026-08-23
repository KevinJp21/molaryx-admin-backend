using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Users.Query.GetTeam
{
    public class GetTeamQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetTeamQuery, PagedResult<GetTeamResponse>>
    {
        public async Task<PagedResult<GetTeamResponse>> Handle(
            GetTeamQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = UserSpec.ForTeam(
                access.IdTenant,
                request.IdUserRoles,
                request.IdUserStatus,
                request.Search);

            var (totalItems, users) = await _unitOfWork.UserRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetTeamResponse>
            {
                Items = [.. users.Select(user => new GetTeamResponse
                {
                    IdProfessional = user.Professional?.IdProfessional,
                    IdUser = user.IdUser,
                    IdUserRole = user.IdUserRole,
                    IdUserStatus = user.IdUserStatus,
                    StatusName = user.UserStatus.Name,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    FirstSurname = user.FirstSurname,
                    SecondSurname = user.SecondSurname,
                    IdentificationType = user.IdentificationType.Name,
                    IdIdentificationType = user.IdIdentificationType,
                    IdentificationNumber = user.IdentificationNumber,
                    BirthDate = user.BirthDate.ToString(),
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email
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
