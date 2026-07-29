using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Extensions;
using Infrastructure.Persistence;

namespace Infrastructure.Services
{
    public class SessionService(
        IUnitOfWork unitOfWork,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        AppDbContext dbContext
    ) : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private readonly ITokenService _tokenService = tokenService;

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        private readonly AppDbContext _dbContext =
        dbContext;

        public async Task<(string AuthToken, string RefreshToken)> CreateSessionAsync(
            long idUser,
            short idUserRole,
            long? idTenant,
            string email,
            CancellationToken cancellationToken)
        {

            var currentDate = DateTime.UtcNow;

            var refreshToken = _tokenService.GenerateRefreshToken();

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var httpContext = _httpContextAccessor.HttpContext;

            var session = new UserSession
            {
                IdUser = idUser,
                RefreshTokenHash = refreshTokenHash,
                ExpiresAt = currentDate.AddDays(7),
                Device = httpContext?
                    .Request
                    .Headers
                    .UserAgent
                    .ToString(),
                IpConnection = httpContext?.GetClientIpAddress(),
                LastLogin = currentDate
            };

            await _unitOfWork.UserSessionRepository.AddAsync(
                session,
                cancellationToken
            );

            await _unitOfWork.UserSessionRepository.SaveChangesAsync(
                cancellationToken
            );

            var authToken = _tokenService.GenerateToken(
                idUser,
                idUserRole,
                idTenant,
                email,
                session.IdUserSession,
                currentDate.AddMinutes(15)
            );

            return (
                authToken,
                refreshToken
            );
        }


        public async Task<(string AuthToken, string RefreshToken)> RefreshSessionAsync(
                string refreshToken,
                CancellationToken cancellationToken = default)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var currentDate = DateTime.UtcNow;

            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidCredentialsException(
                    "La sesión no es válida."
                );
            }

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var session = await _unitOfWork.UserSessionRepository
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

            if (session.ExpiresAt <= currentDate)
            {
                throw new InvalidCredentialsException(
                    "La sesión ha expirado."
                );
            }

            var user = await _unitOfWork.UserRepository.GetByIdAsync(
                session.IdUser,
                cancellationToken
            ) ?? throw new NotFoundException(
                "Usuario no encontrado."
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
                ExpiresAt = currentDate.AddDays(7),
                Device = httpContext?
                    .Request
                    .Headers
                    .UserAgent
                    .ToString(),
                IpConnection = httpContext?
                    .GetClientIpAddress()
            };

            await using var transaction =
                await _dbContext.Database.BeginTransactionAsync(
                    cancellationToken
                );

            try
            {
                // Atomically revoke the current session
                var revoked = await _unitOfWork.UserSessionRepository
                    .RevokeSessionAsync(
                        session.IdUser,
                        refreshTokenHash,
                        currentDate,
                        cancellationToken
                    );

                if (!revoked)
                {
                    throw new InvalidCredentialsException(
                        "La sesión ya no es válida."
                    );
                }

                // Create the new session
                await _unitOfWork.UserSessionRepository.AddAsync(
                    newSession,
                    cancellationToken
                );

                // Save the revoked session and the new session
                await _unitOfWork.UserSessionRepository.SaveChangesAsync(
                    cancellationToken
                );

                // Commit both operations
                await transaction.CommitAsync(
                    cancellationToken
                );

                var authToken = _tokenService.GenerateToken(
                    user.IdUser,
                    user.IdUserRole,
                    user.IdTenant,
                    user.Email,
                    newSession.IdUserSession,
                    currentDate.AddMinutes(15)
                );

                return (
                    authToken,
                    newRefreshToken
                );
            }
            catch
            {
                // Roll back the transaction if any operation fails
                await transaction.RollbackAsync(
                    cancellationToken
                );

                throw;
            }
        }
        public async Task RevokeSessionAsync(
            long idUser,
            string refreshToken,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidCredentialsException(
                    "El refresh token es requerido."
                );
            }

            var currentDate = DateTime.UtcNow;

            var refreshTokenHash =
                _tokenService.HashRefreshToken(refreshToken);

            var revoked = await _unitOfWork.UserSessionRepository.RevokeSessionAsync(
                idUser,
                refreshTokenHash,
                currentDate,
                cancellationToken
            );

            if (!revoked)
            {
                throw new InvalidCredentialsException(
                    "La sesión no es válida o ya fue cerrada."
                );
            }
        }

        public async Task RevokeAllSessionAsync(long idUser, CancellationToken cancellationToken)
        {
            var sessions = await _unitOfWork.UserSessionRepository.GetActiveSessionsByUserIdAsync(idUser, cancellationToken);

            if (!sessions.Any())
                return;

            var currentDate = DateTime.UtcNow;

            foreach (var session in sessions)
            {
                session.RevokedAt = currentDate;
                session.UpdatedAt = currentDate;
            }

            await _unitOfWork.UserSessionRepository.SaveChangesAsync(cancellationToken);
        }

    }
}