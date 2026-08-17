using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Treatment.Command.UpdateTreatment
{
    public class UpdateTreatmentCommandHandler(
        ITreatmentService _treatmentService
    ) : IRequestHandler<UpdateTreatmentCommand, bool>
    {
        public async Task<bool> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
        {
            return await _treatmentService.UpdateTreatmentAsync(request, cancellationToken);
        }
    }
}
