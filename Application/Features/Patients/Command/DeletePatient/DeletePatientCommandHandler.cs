using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Patients.Command.DeletePatient
{
    public class DeletePatientCommandHandler(
        IPatientService _patientService
    ) : IRequestHandler<DeletePatientCommand, bool>
    {
        public Task<bool> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            return _patientService.DeletePatientAsync(request.IdPatient, cancellationToken);
        }
    }
}
