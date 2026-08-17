using Application.Features.Appointment.Command.CreateAppointment;
using Application.Features.Appointment.Command.UpdateAppointment;

namespace Domain.Contracts.IServices
{
    public interface IAppointmentService
    {
        Task<bool> CreateAppointment(CreateAppointmentCommand request, CancellationToken cancellationToken = default);

        Task<bool> UpdateAppointment(UpdateAppointmentCommand request, CancellationToken cancellationToken = default);
    }
}
