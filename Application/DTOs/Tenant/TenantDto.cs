namespace Application.DTOs.Tenant
{
    public class TenantDto
    {
        public short? IdIdentificationType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string IdentificationCode { get; set; } = string.Empty;

        public string ConsultoryName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public OwnerDto Owner { get; set; } = null!;
    }

    public class OwnerDto
    {
        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? SecondName { get; set; }

        public string FirstSurname { get; set; } = string.Empty;

        public string? SecondSurname { get; set; }

        public short IdIdentificationType { get; set; }

        public string IdentificationCode { get; set; } = string.Empty;

        public string IdentificationNumber { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}