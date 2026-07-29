namespace Domain.Contracts.IServices
{
    public interface ITokenService
    {
        string GenerateToken(long idUser, short idUserRole, long? idTenant, string email, long idUserSession, DateTime expiration);
        string GenerateRefreshToken();

        string HashRefreshToken(string refreshToken);
    }
}