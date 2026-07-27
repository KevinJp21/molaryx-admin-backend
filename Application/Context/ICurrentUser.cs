using Domain.Entities;

namespace Application.Context
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        long? IdUser { get; }
        short? IdUserRole { get; }
        long? IdTenant { get; }
        long? IdUserSession { get; }
        string? Email { get; }

        Task<User?> GetUserAsync();
    }

}
