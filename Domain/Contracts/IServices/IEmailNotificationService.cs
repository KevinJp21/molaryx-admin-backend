using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface IEmailNotificationService
    {
        Task SendWelcomeEmailAsync(
            OwnerRegistration owner,
            TenantRegistration tenant,
            CancellationToken cancellationToken = default);

        Task SendAccountActivatedEmailAsync(
            User user,
            Tenant tenant,
            CancellationToken cancellationToken = default);

        Task SendResetPasswordTokenEmailAsync(
            User user,
            string token,
            CancellationToken cancellationToken = default);

        Task SendResetPasswordEmailAsync(
            User user,
            CancellationToken cancellationToken = default);
            
    }
}
