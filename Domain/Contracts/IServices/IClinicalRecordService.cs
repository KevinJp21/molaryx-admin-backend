using Application.Features.ClinicalRecord.Command.CreateClinicalRecord;

namespace Domain.Contracts.IServices
{
    public interface IClinicalRecordService
    {
        Task<bool> CreateClinicalRecordAsync(CreateClinicalRecordCommand request, CancellationToken cancellationToken = default);
    }
}