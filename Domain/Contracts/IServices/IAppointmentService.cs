using Application.Features.Appointment.Command.CreateAppointment;

namespace Domain.Contracts.IServices
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(CreateAppointmentCommand request, CancellationToken cancellationToken = default);
    }
}
