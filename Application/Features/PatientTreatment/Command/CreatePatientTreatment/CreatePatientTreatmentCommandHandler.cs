using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.PatientTreatment.Command.CreatePatientTreatment
{
    public class CreatePatientTreatmentCommandHandler(
        IPatientTreatmentService _patientTreatmentService
    ) : IRequestHandler<CreatePatientTreatmentCommand, bool>
    {
        public Task<bool> Handle(CreatePatientTreatmentCommand request, CancellationToken cancellationToken)
        {
            return _patientTreatmentService.CreatePatientTreatmentAsync(request, cancellationToken);
        }
    }
}
