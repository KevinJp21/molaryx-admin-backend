namespace Application.DTOs.Tenant.TenantRegistration
{
    public class OwnerRegistrationDto
    {
        public string Username { get; set; } = string.Empty;
        
        public string FirstName { get; set; } = string.Empty;

        public string? SecondName { get; set; }

        public string FirstSurname { get; set; } = string.Empty;

        public string? SecondSurname { get; set; }

        public short IdIdentificationType { get; set; }

        public string IdentificationNumber { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}