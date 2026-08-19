namespace Application.Features.ClinicalRecord.Query.GetClinicalHistory
{
    public sealed class ClinicalHistoryFileResult
    {
        public required byte[] Content { get; init; }
        public required string FileName { get; init; }
    }
}
