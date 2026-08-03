namespace Domain.Contracts.IServices
{
    public interface IPasswordResetTokenService
    {
        Task<bool> SendResetPasswordTokenAsync(string email, CancellationToken cancellationToken = default);
    }
}