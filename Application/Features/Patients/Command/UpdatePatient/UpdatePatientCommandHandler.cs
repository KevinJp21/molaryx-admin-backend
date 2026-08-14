using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Patients.Command.UpdatePatient
{
    public class UpdatePatientCommandHandler(
        IPatientService _patientService
    ) : IRequestHandler<UpdatePatientCommand, bool>
    {
        public Task<bool> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            return _patientService.UpdatePatientAsync(request, cancellationToken);
        }
    }
}
