using Application.Common.Mediator.Interfaces;

namespace Application.Features.Tenant.Command.ActivateTenant
{
    public class ActivateTenantCommand : IRequest<bool>
    {
        public long IdUser { get; set; }
        public long IdTenant { get; set; }
        public long IdTenantSubscription { get; set; }
    }
}