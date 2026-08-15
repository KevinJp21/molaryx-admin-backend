using Application.Common.Mediator.Interfaces;
using Application.Features.Appointment.Command;
using Domain.Contracts.IServices;

namespace Application.Features.Appointment.Command
{
    public class CreateAppointmentCommandHandler(
        IAppointmentService appointmentService
    ) : IRequestHandler<CreateAppointmentCommand, bool>
    {
        public Task<bool> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            return appointmentService.CreateAppointment(request, cancellationToken);
        }
    }
}
