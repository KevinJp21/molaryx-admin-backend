namespace Domain.Contracts.IServices
{
    public interface ISessionService
    {
        Task<(string AuthToken, string RefreshToken)> CreateSessionAsync(long idUser, short idUserRole, string email, CancellationToken cancellationToken);

        Task<(string AuthToken, string RefreshToken)> RefreshSessionAsync(
        CancellationToken cancellationToken
    );

        Task RevokeSessionAsync(CancellationToken cancellationToken);
    }
}