namespace Domain.Contracts.IServices
{
    public interface IPasswordResetTokenService
    {
        Task<bool> SendResetPasswordTokenAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> ResetPasswordAsync(
            string password,
            string confirmPassword,
            string token,
            CancellationToken cancellationToken = default);
    }
}