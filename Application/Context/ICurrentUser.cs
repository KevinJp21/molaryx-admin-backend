using Domain.Entities;

namespace Application.Context
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        long? IdUser { get; }
        long? IdUserSession { get; }
        string? Email { get; }

        Task<User?> GetUserAsync();
    }

}
