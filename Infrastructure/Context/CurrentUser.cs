using Application.Common;
using Application.Context;
using Domain.Contracts;
using Domain.Contracts.IRepositories;
using Domain.Entities;
using System.Security.Claims;

namespace Infrastructure.Context
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork) : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

        private long? _cachedIdUserSession;
        public long? IdUserSession
        {
            get
            {
                if (_cachedIdUserSession.HasValue)
                    return _cachedIdUserSession;

                if (!IsAuthenticated)
                    return null;

                var sessionClaim =
                    User?.FindFirst(AuthClaimTypes.IdUserSession)?.Value;

                if (string.IsNullOrWhiteSpace(sessionClaim))
                    return null;

                if (!long.TryParse(sessionClaim, out var idUserSession))
                    return null;

                _cachedIdUserSession = idUserSession;
                return idUserSession;
            }
        }

        private long? _cachedIdTenant;
        public long? IdTenant
        {
            get
            {
                if (_cachedIdTenant.HasValue)
                    return _cachedIdTenant;

                if (!IsAuthenticated)
                    return null;

                var tenantClaim =
                    User?.FindFirst(AuthClaimTypes.IdTenant)?.Value;

                if (string.IsNullOrWhiteSpace(tenantClaim))
                    return null;

                if (!long.TryParse(tenantClaim, out var idTenant))
                    return null;

                _cachedIdTenant = idTenant;
                return idTenant;
            }
        }

        private short? _cachedIdUserRole;
        public short? IdUserRole
        {
            get
            {
                if (_cachedIdUserRole.HasValue)
                    return _cachedIdUserRole;

                if (!IsAuthenticated)
                    return null;

                var roleClaim =
                    User?.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrWhiteSpace(roleClaim))
                    return null;

                if (!short.TryParse(roleClaim, out var idUserRole))
                    return null;

                _cachedIdUserRole = idUserRole;
                return idUserRole;
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

            _cachedUser = await _unitOfWork.UserRepository.GetByIdAsync(IdUser.Value);
            return _cachedUser;
        }
    }
}
