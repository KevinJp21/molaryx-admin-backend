using Application.Common.Mediator.Interfaces;
using Application.DTOs.Tenant.TenantRegistration;

namespace Application.Features.RegisterTenant.Command
{
    public class RegisterTenantCommand : IRequest<bool>
    {
        public short IdPlan { get; set; }
        public OwnerRegistrationDto Owner { get; set; } = null!;
        public TenantRegistrationDto Tenant { get; set; } = null!;
    }
}