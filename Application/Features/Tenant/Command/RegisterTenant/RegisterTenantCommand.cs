using Application.Common.Interfaces;
using Application.Common.Mediator.Interfaces;

namespace Application.Features.Tenant.Command.RegisterTenant
{
    public class RegisterTenantCommand : IRequest<bool>
    {
        public short IdPlan { get; set; }
        public long? IdPromotion { get; set; }
        public TenantRegistration Tenant { get; set; } = null!;
        public OwnerRegistration Owner { get; set; } = null!;
    }

        
    public class TenantRegistration : ITenantRegistration
    {
        public short? IdIdentificationType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string ConsultoryName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }

    public class OwnerRegistration : IOwnerRegistration
    {
        public string Username { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? SecondName { get; set; }

        public string FirstSurname { get; set; } = string.Empty;

        public string? SecondSurname { get; set; }

        public short IdIdentificationType { get; set; }

        public string IdentificationNumber { get; set; } = string.Empty;

        public DateOnly BirthDate { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}

