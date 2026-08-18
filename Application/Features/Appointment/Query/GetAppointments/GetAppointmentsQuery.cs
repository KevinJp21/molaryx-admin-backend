using Application.Common.Mediator.Interfaces;

namespace Application.Features.Appointment.Query.GetAppointments
{
    public class GetAppointmentsQuery : IRequest<GetAppointmentsResponse[]>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public long? IdProfessional { get; set; }
    }
}
