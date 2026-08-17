using Application.Common.Mediator.Interfaces;
using Application.Features.Treatment.Command.DeleteTreatment;
using Domain.Contracts.IServices;

namespace Application.Features.Treatment.Command.DeleteTreatment
{
    public class DeleteTreatmentCommandHandler(
        ITreatmentService _treatmentService
    ) : IRequestHandler<DeleteTreatmentCommand, bool>
    {
        public async Task<bool> Handle(DeleteTreatmentCommand request, CancellationToken cancellationToken)
        {
            return await _treatmentService.DeleteTreatmentAsync(request.IdTreatment, cancellationToken);
        }
    }
}
