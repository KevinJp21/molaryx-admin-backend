using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;

namespace Application.Features.Appointment.Query.GetAppointmentsList
{
    public class GetAppointmentsListQuery : PageFilter, IRequest<PagedResult<GetAppointmentsListResponse>>
    {
        public long? IdPatient { get; set; }
        public long? IdProfessional { get; set; }
        public short? IdAppointmentStatus { get; set; }
    }
}
