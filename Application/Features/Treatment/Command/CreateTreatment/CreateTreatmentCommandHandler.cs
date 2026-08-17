using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Treatment.Command.CreateTreatment
{
    public class CreateTreatmentCommandHandler(
        ITreatmentService _treatmentService
    ) : IRequestHandler<CreateTreatmentCommand, bool>
    {
        public Task<bool> Handle(CreateTreatmentCommand request, CancellationToken cancellationToken)
        {
            return _treatmentService.CreateTreatmentAsync(request, cancellationToken);
        }
    }
}
