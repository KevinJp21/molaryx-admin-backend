using Application.Common.Interfaces;
using Application.Common.Mediator.Interfaces;

namespace Application.Features.Platform.Tenant.Command.CreateBusinessTenant
{
    public class CreateBusinessTenantCommand : IRequest<bool>
    {
        public BusinessPlanRequest Plan { get; set; } = null!;
        public BusinessTenantRequest Tenant { get; set; } = null!;
        public BusinessOwnerRequest Owner { get; set; } = null!;
    }

    public class BusinessPlanRequest
    {
        public decimal Price { get; set; }
        public short MaxProfessionals { get; set; }
        public short MaxAssistants { get; set; }
        public int MaxPatients { get; set; }
    }

    public class BusinessTenantRequest : ITenantRegistration
    {
        public short? IdIdentificationType { get; set; }

        public string? IdentificationNumber { get; set; }

        public string ConsultoryName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
    }

    public class BusinessOwnerRequest : IOwnerRegistration
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

