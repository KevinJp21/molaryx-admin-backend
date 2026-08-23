namespace Application.Features.ClinicalRecord.Query.GetClinicalRecords
{
    public class GetClinicalRecordsResponse
    {
        public long IdClinicalRecord { get; set; }
        public long? IdAppointment { get; set; }
        public long? IdPatientTreatment { get; set; }
        public long? IdProcedure { get; set; }
        public string? ProcedureName { get; set; }
        public DateTime RecordedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Evolution { get; set; }
        public string? Notes { get; set; }
        public ClinicalRecordPatient Patient { get; set; } = null!;
        public ClinicalRecordCreatedBy CreatedBy { get; set; } = null!;
        public ClinicalRecordAppointment? Appointment { get; set; }
        public ClinicalRecordPatientTreatment? PatientTreatment { get; set; }
    }

    public class ClinicalRecordPatient
    {
        public long IdPatient { get; set; }
        public string IdentificationType { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class ClinicalRecordCreatedBy
    {
        public long IdUser { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }

    public class ClinicalRecordAppointment
    {
        public long IdAppointment { get; set; }
        public string ProcedureNames { get; set; } = string.Empty;
        public short IdAppointmentStatus { get; set; }
        public string AppointmentStatus { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string ProfessionalName { get; set; } = string.Empty;
        public string ProfessionalSurname { get; set; } = string.Empty;
    }

    public class ClinicalRecordPatientTreatment
    {
        public long IdPatientTreatment { get; set; }
        public long IdTreatment { get; set; }
        public string TreatmentName { get; set; } = string.Empty;
        public decimal? AgreedPrice { get; set; }
        public short IdPatientTreatmentStatus { get; set; }
        public string PatientTreatmentStatus { get; set; } = string.Empty;
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
