namespace Domain.Models
{
    public class ClinicalHistoryTemplateInformation
    {
        public string ConsultoryName { get; set; } = string.Empty;
        public string? TenantIdentificationType { get; set; }
        public string? TenantIdentificationNumber { get; set; }
        public string TenantEmail { get; set; } = string.Empty;
        public string TenantPhoneNumber { get; set; } = string.Empty;
        public string TenantAddress { get; set; } = string.Empty;

        public string PatientIdentificationType { get; set; } = string.Empty;
        public string PatientIdentificationNumber { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string PatientSurname { get; set; } = string.Empty;
        public string PatientEmail { get; set; } = string.Empty;
        public string PatientPhoneNumber { get; set; } = string.Empty;
        public DateOnly PatientBirthDate { get; set; }

        public DateOnly? From { get; set; }
        public DateOnly? To { get; set; }
        public DateTime GeneratedAt { get; set; }

        public List<ClinicalHistoryRecordInformation> Records { get; set; } = [];
    }

    public class ClinicalHistoryRecordInformation
    {
        public DateTime RecordedAt { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Diagnosis { get; set; }
        public string? Evolution { get; set; }
        public string? Notes { get; set; }
        public string CreatedByName { get; set; } = string.Empty;
        public string CreatedBySurname { get; set; } = string.Empty;
        public string? Reference { get; set; }
        public string? ServiceName { get; set; }
    }
}
