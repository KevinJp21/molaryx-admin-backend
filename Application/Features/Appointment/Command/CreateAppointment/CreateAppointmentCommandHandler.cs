using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Appointment.Command.CreateAppointment
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
