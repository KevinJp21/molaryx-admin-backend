namespace Application.Features.Users.Query.GetProfessionals
{
    public class GetProfessionalsResponse
    {
        public long IdProfessional { get; set; }
        public short IdUserStatus { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; } = string.Empty;
        public string? SecondSurname { get; set; }
        public string IdentificationType { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}