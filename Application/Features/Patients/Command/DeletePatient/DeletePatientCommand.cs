using Application.Common.Mediator.Interfaces;

namespace Application.Features.Patients.Command.DeletePatient
{
    public class DeletePatientCommand : IRequest<bool>
    {
        public long IdPatient { get; set; }
    }
}