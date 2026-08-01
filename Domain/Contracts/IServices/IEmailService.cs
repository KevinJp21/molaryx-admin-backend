namespace Domain.Contracts.IServices
{
    public interface IEmailService
    {
        Task SendAsync(
            string from,
            string to,
            string subject,
            string html,
            CancellationToken cancellationToken
        );
    }
}