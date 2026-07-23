namespace Domain.Contracts.IServices
{
    public interface ITokenService
    {
        string GenerateToken(long idUser, string email, DateTime expiration);
    }
}