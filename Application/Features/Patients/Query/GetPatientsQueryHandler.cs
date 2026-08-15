using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Patients.Query
{
    public class GetPatientsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetPatientsQuery, PagedResult<GetPatientsResponse>>
    {
        public async Task<PagedResult<GetPatientsResponse>> Handle(GetPatientsQuery request, CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new PatientsSpec(access.IdTenant);

            var (totalItems, patients) = await _unitOfWork.PatientsRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetPatientsResponse>
            {
                Items = [.. patients.Select(patient => new GetPatientsResponse
                {
                    IdPatient = patient.IdPatient,
                    IdIdentificationType = patient.IdIdentificationType,
                    IdentificationType = patient.IdentificationType.Name,
                    IdentificationNumber = patient.IdentificationNumber,
                    FirstName = patient.FirstName,
                    SecondName = patient.SecondName!,
                    FirstSurname = patient.FirstSurname,
                    SecondSurname = patient.SecondSurname!,
                    BirthDate = patient.BirthDate,
                    PhoneNumber = patient.PhoneNumber!,
                    Email = patient.Email!,
                    IsActive = patient.IsActive,
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
