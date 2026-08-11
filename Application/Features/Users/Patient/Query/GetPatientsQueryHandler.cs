using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Application.Context;
using Domain.Contracts;

namespace Application.Features.Users.Patient.Query
{
    public class GetPatientsQueryHandler(
        IUnitOfWork _unitOfWork,
        ICurrentUser _currentUser
    ) : IRequestHandler<GetPatientsQuery, PagedResult<GetPatientsResponse>>
    {
        public async Task<PagedResult<GetPatientsResponse>> Handle(GetPatientsQuery request, CancellationToken cancellationToken = default)
        {
            var idTenant = _currentUser.IdTenant
                ?? throw new InvalidOperationException(
                    "El usuario no pertenece a un consultorio."
                );

            var (totalItems, patients) = await _unitOfWork.PatientsRepository.GetAllPatientsByIdTenantAsync(
                idTenant,
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                null,
                cancellationToken
            );

            return new PagedResult<GetPatientsResponse>
            {
                Items = [.. patients.Select(patient => new GetPatientsResponse
                {
                    IdPatient = patient.IdPatient,
                    IdIdentificationType = patient.IdIdentificationType,
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