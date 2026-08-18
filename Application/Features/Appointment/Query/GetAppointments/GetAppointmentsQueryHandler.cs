using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Appointment.Query.GetAppointments
{
    public class GetAppointmentsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetAppointmentsQuery, GetAppointmentsResponse[]>
    {
        public async Task<GetAppointmentsResponse[]> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = AppointmentSpec.ForRange(access.IdTenant, request.From, request.To, request.IdProfessional);

            var appointments = await _unitOfWork.AppointmentRepository.GetAll(spec, cancellationToken) ?? [];

            return [.. appointments.Select(appointment => new GetAppointmentsResponse
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
                IdAppointmentStatus = appointment.IdAppointmentStatus,
                AppointmentStatus = appointment.AppointmentStatus.Name,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                Notes = appointment.Notes ?? string.Empty
            })];
        }
    }
}
