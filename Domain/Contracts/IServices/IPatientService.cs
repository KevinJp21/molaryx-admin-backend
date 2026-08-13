using Application.Features.Patients.Command.CreatePatient;

namespace Domain.Contracts.IServices
{
    public interface IPatientService
    {
        Task<bool> CreatePatientAsync(
            CreatePatientCommand command,
            CancellationToken cancellationToken
        );
    }
}