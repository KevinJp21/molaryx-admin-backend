using Application.Features.Treatment.Command.CreateTreatment;

namespace Domain.Contracts.IServices
{
    public interface ITreatmentService
    {
        Task<bool> CreateTreatmentAsync(CreateTreatmentCommand request, CancellationToken cancellationToken);
    }
}
