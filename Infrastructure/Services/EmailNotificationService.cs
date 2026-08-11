using Application.Features.Auth.Command.RegisterTenant;
using Domain.Constants;
using Domain.Contracts.IServices;
using Domain.Entities;
using Infrastructure.Email;

namespace Infrastructure.Services
{
    public class EmailNotificationService(IEmailService _emailService, IConfiguration _configuration) : IEmailNotificationService
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
                    ["ConsultoryName"] = tenant.ConsultoryName
                }
            );

            await _emailService.SendAsync(
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
                    ["ConsultoryName"] = tenant.ConsultoryName
                }
            );

            await _emailService.SendAsync(
                EmailFrom.NoReply,
                user.Email,
                "¡Tu cuenta ha sido activada!",
                html,
                cancellationToken
            );
        }

        public async Task SendResetPasswordTokenEmailAsync(
            User user,
            string token,
            CancellationToken cancellationToken = default
        )
        {
            var firstName = user.FirstName.Trim().Split(' ')[0];
            var html = EmailTemplateService.RenderTemplate(
                "ResetPasswordToken",
                new Dictionary<string, string>
                {
                    ["Name"] = firstName,
                    ["ResetUrl"] = _configuration["App:FrontendUrl"] + _configuration["App:ResetPasswordUrl"] + $"?token={token}",
                }
            );

            await _emailService.SendAsync(
                EmailFrom.NoReply,
                user.Email,
                "Restablece tu contraseña",
                html,
                cancellationToken
            );
        }

        public async Task SendResetPasswordEmailAsync(
            User user,
            CancellationToken cancellationToken = default)
        {

            var firstName = user.FirstName.Trim().Split(' ')[0];
            var html = EmailTemplateService.RenderTemplate(
                "ResetPassword",
                new Dictionary<string, string>
                {
                    ["Name"] = firstName,
                }
            );

            await _emailService.SendAsync(
                EmailFrom.NoReply,
                user.Email,
                "Restablece tu contraseña",
                html,
                cancellationToken
            );
        }
    }

}
