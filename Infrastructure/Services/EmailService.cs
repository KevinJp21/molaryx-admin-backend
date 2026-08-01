using Domain.Contracts.IServices;
using Resend;

namespace Infrastructure.Services;

public class EmailService(IResend resend) : IEmailService
{
    public async Task SendAsync(
        string from,
        string to,
        string subject,
        string html,
        CancellationToken cancellationToken = default
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(from);
        ArgumentException.ThrowIfNullOrWhiteSpace(to);
        ArgumentException.ThrowIfNullOrWhiteSpace(subject);
        ArgumentException.ThrowIfNullOrWhiteSpace(html);

        var message = new EmailMessage
        {
            From = from,
            To = [to],
            Subject = subject,
            HtmlBody = html
        };

        await resend.EmailSendAsync(
            message,
            cancellationToken
        );
    }
}