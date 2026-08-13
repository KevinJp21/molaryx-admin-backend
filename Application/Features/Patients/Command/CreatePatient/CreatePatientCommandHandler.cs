using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Patients.Command.CreatePatient
{
    public class CreatePatientCommandHandler(
        IPatientService _patientService
    ) : IRequestHandler<CreatePatientCommand, bool>
    {
        public Task<bool> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            return _patientService.CreatePatientAsync(request, cancellationToken);
        }
    }
}
