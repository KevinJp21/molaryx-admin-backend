namespace Domain.Contracts.IServices
{
    public interface ISessionService
    {
        Task<(string AuthToken, string RefreshToken)> CreateSessionAsync(long idUser, short idUserRole, string email, CancellationToken cancellationToken);

        Task<(string AuthToken, string RefreshToken)> RefreshSessionAsync(
            string refreshToken,
            CancellationToken cancellationToken = default);

        Task RevokeSessionAsync(string refreshToken, CancellationToken cancellationToken = default);

        Task RevokeAllSessionAsync(long idUser, CancellationToken cancellationToken = default);
    }
}