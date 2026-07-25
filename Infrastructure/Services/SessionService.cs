using Domain.Contracts.IRepositories;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Extensions;

namespace Infrastructure.Services
{
    public class SessionService(
        IUserSessionRepository userSessionRepository,
        IUserRepository userRepository,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor
    ) : ISessionService
    {
        private readonly IUserSessionRepository _userSessionRepository =
            userSessionRepository;

        private readonly IUserRepository _userRepository =
            userRepository;

        private readonly ITokenService _tokenService =
            tokenService;

        private readonly IHttpContextAccessor _httpContextAccessor =
            httpContextAccessor;

        public async Task<(string AuthToken, string RefreshToken)> CreateSessionAsync(
            long idUser,
            short idUserRole,
            string email,
            CancellationToken cancellationToken)
        {
            var authToken = _tokenService.GenerateToken(
                idUser,
                idUserRole,
                email,
                DateTime.UtcNow.AddMinutes(15)
            );

            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var httpContext = _httpContextAccessor.HttpContext;

            var session = new UserSession
            {
                IdUser = idUser,
                RefreshTokenHash = refreshTokenHash,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Device = httpContext?
                    .Request
                    .Headers
                    .UserAgent
                    .ToString(),
                IpConnection = httpContext?.GetClientIpAddress()
            };

            await _userSessionRepository.AddAsync(
                session,
                cancellationToken
            );

            await _userSessionRepository.SaveChangesAsync(
                cancellationToken
            );

            return (
                authToken,
                refreshToken
            );
        }

        public async Task RevokeSessionAsync(
            string refreshToken,
            CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(refreshToken))
                return;

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var session = await _userSessionRepository
                .GetByRefreshTokenHashAsync(
                    refreshTokenHash,
                    cancellationToken
                );

            if (session is null)
                return;

            if (session.RevokedAt.HasValue)
                return;

            session.RevokedAt = DateTime.UtcNow;

            await _userSessionRepository.SaveChangesAsync(
                cancellationToken
            );
        }

        public async Task<(string AuthToken, string RefreshToken)> RefreshSessionAsync(
            string refreshToken,
            CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidCredentialsException(
                    "La sesión no es válida."
                );
            }

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var session = await _userSessionRepository
                .GetByRefreshTokenHashAsync(
                    refreshTokenHash,
                    cancellationToken
                );

            if (session is null)
            {
                throw new InvalidCredentialsException(
                    "La sesión no es válida."
                );
            }

            if (session.RevokedAt.HasValue)
            {
                throw new InvalidCredentialsException(
                    "La sesión ha sido revocada."
                );
            }

            if (session.ExpiresAt <= DateTime.UtcNow)
            {
                throw new InvalidCredentialsException(
                    "La sesión ha expirado."
                );
            }

            var user = await _userRepository.GetByIdAsync(
                session.IdUser,
                cancellationToken
            ) ?? throw new NotFoundException(
                "Usuario no encontrado."
            );

            // Revoke current session
            session.RevokedAt = DateTime.UtcNow;

            // Generate new access token
            var authToken = _tokenService.GenerateToken(
                user.IdUser,
                user.IdUserRole,
                user.Email,
                DateTime.UtcNow.AddMinutes(15)
            );

            // Generate new refresh token
            var newRefreshToken =
                _tokenService.GenerateRefreshToken();

            var newRefreshTokenHash =
                _tokenService.HashRefreshToken(
                    newRefreshToken
                );

            var newSession = new UserSession
            {
                IdUser = user.IdUser,
                RefreshTokenHash = newRefreshTokenHash,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Device = httpContext?
                    .Request
                    .Headers
                    .UserAgent
                    .ToString(),
                IpConnection = httpContext?.GetClientIpAddress()
            };

            await _userSessionRepository.AddAsync(
                newSession,
                cancellationToken
            );

            await _userSessionRepository.SaveChangesAsync(
                cancellationToken
            );

            return (
                authToken,
                newRefreshToken
            );
        }
    }
}