using Domain.Entities;

namespace Application.Context
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        long? UserId { get; }
        string? Email { get; }

        Task<User?> GetUserAsync();
    }

}
