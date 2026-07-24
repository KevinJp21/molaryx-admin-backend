namespace Domain.Contracts.IServices
{
    public interface ITokenService
    {
        string GenerateToken(long idUser, short idUserRole, string email, DateTime expiration);
    }
}