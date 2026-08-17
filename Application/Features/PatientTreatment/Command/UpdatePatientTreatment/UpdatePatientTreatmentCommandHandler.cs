using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.PatientTreatment.Command.UpdatePatientTreatment
{
    public class UpdatePatientTreatmentCommandHandler(
        IPatientTreatmentService _patientTreatmentService
    ) : IRequestHandler<UpdatePatientTreatmentCommand, bool>
    {
        public Task<bool> Handle(UpdatePatientTreatmentCommand request, CancellationToken cancellationToken)
        {
            return _patientTreatmentService.UpdatePatientTreatmentAsync(request, cancellationToken);
        }
    }
}
