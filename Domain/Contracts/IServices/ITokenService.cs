namespace Domain.Contracts.IServices
{
    public interface ITokenService
    {
        string GenerateJwt(long idUser, short idUserRole, long? idTenant, string email, long idUserSession, DateTime expiration);
        string GenerateToken();
        string GenerateResetPasswordToken();
        string HashRefreshToken(string refreshToken);
    }
}