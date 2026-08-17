using Application.Features.Treatment.Command.CreateTreatment;
using Application.Features.Treatment.Command.UpdateTreatment;

namespace Domain.Contracts.IServices
{
    public interface ITreatmentService
    {
        Task<bool> CreateTreatmentAsync(CreateTreatmentCommand request, CancellationToken cancellationToken);

        Task<bool> UpdateTreatmentAsync(UpdateTreatmentCommand request, CancellationToken cancellationToken);

        Task<bool> DeleteTreatmentAsync(long idTreatment, CancellationToken cancellationToken);
    }
}
