namespace Application.Features.Users.Query.GetProfile
{
    public class GetProfileQueryResponse
    {
        public long IdUser { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? SecondName { get; set; }
        public string FirstSurname { get; set; } = string.Empty;
        public string? SecondSurname { get; set; }
        public short IdIdentificationType { get; set; }
        public string IdentificationType { get; set; } = string.Empty;
        public string IdentificationNumber { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public short IdUserRole { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public short IdUserStatus { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public ProfileTenantSummary? Tenant { get; set; }
    }

    public class ProfileTenantSummary
    {
        public string ConsultoryName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public short? IdIdentificationType { get; set; }
        public string? IdentificationType { get; set; }
        public string? IdentificationNumber { get; set; }
    }
}
