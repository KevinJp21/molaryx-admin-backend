using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Appointment.Command.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler(
        IAppointmentService appointmentService
    ) : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        public Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            return appointmentService.UpdateAppointment(request, cancellationToken);
        }
    }
}
