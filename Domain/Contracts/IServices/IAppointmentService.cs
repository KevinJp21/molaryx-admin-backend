using Application.Features.Appointment.Command;

namespace Domain.Contracts.IServices
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(CreateAppointmentCommand request, CancellationToken cancellationToken = default);
    }
}
