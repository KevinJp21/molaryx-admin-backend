namespace Application.Features.Patients.Query
{
    public class GetPatientsResponse
    {
        public long IdPatient { get; set; }
        public short IdIdentificationType { get; set; }
        public string IdentificationType { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; } = string.Empty;
        public string? SecondSurname { get; set; }
        public DateOnly BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
