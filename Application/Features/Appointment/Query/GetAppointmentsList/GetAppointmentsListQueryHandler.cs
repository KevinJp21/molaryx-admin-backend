using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Appointment.Query.GetAppointmentsList
{
    public class GetAppointmentsListQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetAppointmentsListQuery, PagedResult<GetAppointmentsListResponse>>
    {
        public async Task<PagedResult<GetAppointmentsListResponse>> Handle(
            GetAppointmentsListQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = AppointmentSpec.ForList(
                access.IdTenant,
                request.IdPatient,
                request.IdProfessional,
                request.IdAppointmentStatus);

            var (totalItems, appointments) = await _unitOfWork.AppointmentRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetAppointmentsListResponse>
            {
                Items = [.. appointments.Select(appointment => new GetAppointmentsListResponse
                {
                    IdAppointment = appointment.IdAppointment,
                    IdPatient = appointment.IdPatient,
                    PatientName = $"{appointment.Patient.FirstName}{(!string.IsNullOrEmpty(appointment.Patient.SecondName) ? $" {appointment.Patient.SecondName}" : string.Empty)}",
                    PatientSurname = $"{appointment.Patient.FirstSurname}{(!string.IsNullOrEmpty(appointment.Patient.SecondSurname) ? $" {appointment.Patient.SecondSurname}" : string.Empty)}",
                    IdProfessional = appointment.IdProfessional,
                    IdUser = appointment.Professional.IdUser,
                    ProfessionalName = $"{appointment.Professional.User.FirstName}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondName) ? $" {appointment.Professional.User.SecondName}" : string.Empty)}",
                    ProfessionalSurname = $"{appointment.Professional.User.FirstSurname}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondSurname) ? $" {appointment.Professional.User.SecondSurname}" : string.Empty)}",
                    IdService = appointment.IdService,
                    ServiceName = appointment.Service.Name,
                    IdPatientTreatment = appointment.IdPatientTreatment,
                    PatientTreatmentName = appointment.PatientTreatment?.Treatment?.Name,
                    Price = appointment.Price,
                    IdAppointmentStatus = appointment.IdAppointmentStatus,
                    AppointmentStatus = appointment.AppointmentStatus.Name,
                    StartAt = appointment.StartAt,
                    EndAt = appointment.EndAt,
                    Notes = appointment.Notes
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
