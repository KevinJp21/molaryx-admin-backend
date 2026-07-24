using Application.Context;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using System.Security.Claims;

namespace Infrastructure.Context
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository) : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IUserRepository _userRepository = userRepository;

        private User? _cachedUser;

        private ClaimsPrincipal? User =>
            _httpContextAccessor.HttpContext?.User;

        public bool IsAuthenticated =>
            User?.Identity?.IsAuthenticated ?? false;

        private long? _cachedIdUser;
        public long? IdUser
        {
            get
            {
                if (_cachedIdUser.HasValue)
                    return _cachedIdUser;

                if (!IsAuthenticated)
                    return null;

                var idClaim =
                    User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                    ?? User?.FindFirst("sub")?.Value;

                if (string.IsNullOrWhiteSpace(idClaim))
                    return null;

                if (!long.TryParse(idClaim, out var idUser))
                    return null;

                _cachedIdUser = idUser;
                return idUser;
            }
        }

        public string? Email =>
            User?.FindFirst(ClaimTypes.Email)?.Value;

        public async Task<User?> GetUserAsync()
        {
            if (!IsAuthenticated || IdUser == null)
                return null;

            if (_cachedUser != null)
                return _cachedUser;

            _cachedUser = await _userRepository.GetByIdAsync(IdUser.Value);
            return _cachedUser;
        }
    }
}
