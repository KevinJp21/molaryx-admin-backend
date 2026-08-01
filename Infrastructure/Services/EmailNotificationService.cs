using Application.Features.Tenant.Command.RegisterTenant;
using Domain.Constants;
using Domain.Contracts.IServices;
using Domain.Entities;
using Infrastructure.Email;

namespace Infrastructure.Services
{
    public class EmailNotificationService(IEmailService emailService) : IEmailNotificationService
    {
        public async Task SendWelcomeEmailAsync(
            OwnerRegistration owner,
            TenantRegistration tenant,
            CancellationToken cancellationToken = default)
        {
            var firstName = owner.FirstName.Trim().Split(' ')[0];

            var html = EmailTemplateService.RenderTemplate(
                "Welcome",
                new Dictionary<string, string>
                {
                    ["Name"] = firstName,
                    ["ConsultoryName"] = tenant.ConsultoryName,
                    ["Year"] = DateTime.UtcNow.Year.ToString()
                }
            );

            await emailService.SendAsync(
                EmailFrom.NoReply,
                owner.Email,
                "¡Bienvenido a Molaryx!",
                html,
                cancellationToken
            );
        }
        
         public async Task SendAccountActivatedEmailAsync(
            User user,
            Tenant tenant,
            CancellationToken cancellationToken = default)
        {
            var firstName = user.FirstName.Trim().Split(' ')[0];

            var html = EmailTemplateService.RenderTemplate(
                "AccountActivated",
                new Dictionary<string, string>
                {
                    ["Name"] = firstName,
                    ["ConsultoryName"] = tenant.ConsultoryName,
                    ["Year"] = DateTime.UtcNow.Year.ToString()
                }
            );

            await emailService.SendAsync(
                EmailFrom.NoReply,
                user.Email,
                "¡Tu cuenta ha sido activada!",
                html,
                cancellationToken
            );
        }
    }

}
