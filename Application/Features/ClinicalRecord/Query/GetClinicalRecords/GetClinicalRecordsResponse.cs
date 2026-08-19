namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsResponse
    {
        public long IdClinicalRecord { get; set; }
        public long IdPatient { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public string? TreatmentName { get; set; }
        public long? IdService { get; set; }
        public string? ServiceName { get; set; }
        public string CreatedByUser { get; set; } = string.Empty;
        public DateTime RecordedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Evolution { get; set; }
        public string? Notes { get; set; }
    }
}
