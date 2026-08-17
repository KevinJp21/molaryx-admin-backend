using Application.Features.PatientTreatment.Command.CreatePatientTreatment;

namespace Domain.Contracts.IServices
{
    public interface IPatientTreatmentService
    {
        Task<bool> CreatePatientTreatmentAsync(
            CreatePatientTreatmentCommand request,
            CancellationToken cancellationToken);
    }
}
