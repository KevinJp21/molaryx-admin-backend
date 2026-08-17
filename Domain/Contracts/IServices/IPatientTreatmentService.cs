using Application.Features.PatientTreatment.Command.CreatePatientTreatment;
using Application.Features.PatientTreatment.Command.UpdatePatientTreatment;

namespace Domain.Contracts.IServices
{
    public interface IPatientTreatmentService
    {
        Task<bool> CreatePatientTreatmentAsync(
            CreatePatientTreatmentCommand request,
            CancellationToken cancellationToken);

        Task<bool> UpdatePatientTreatmentAsync(
            UpdatePatientTreatmentCommand request,
            CancellationToken cancellationToken);
    }
}
