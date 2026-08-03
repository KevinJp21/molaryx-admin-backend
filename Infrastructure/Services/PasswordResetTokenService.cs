using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class PasswordResetTokenService(
        IUnitOfWork _unitOfWork,
        ITokenService _tokenService,
        IEmailNotificationService _emailNotificationService,
        IHasherService _hasherService
        ) : IPasswordResetTokenService
    {
        public async Task<bool> SendResetPasswordTokenAsync(string email, CancellationToken cancellationToken = default)
        {
            var currentDate = DateTime.UtcNow;
            var user = await _unitOfWork.UserRepository.GetByEmailAsync(email, cancellationToken);

            if (user is null || user.IdUserStatus != (short)UserStatusEnum.ACTIVE)
            {
                return true;
            }

            var activeTokens = await _unitOfWork.PasswordResetTokenRepository.GetActiveTokensByUserIdAsync(user.IdUser, cancellationToken);

            if (activeTokens.Count > 0)
            {
                foreach (var activeToken in activeTokens)
                {
                    activeToken.UsedAt = currentDate;
                }
            }

            var token = _tokenService.GenerateResetPasswordToken();
            var tokenHash = _tokenService.HashToken(token);

            var passwordResetToken = new PasswordResetToken
            {
                IdUser = user.IdUser,
                Token = tokenHash,
                ExpiresAt = currentDate.AddMinutes(15),
                CreatedAt = currentDate
            };

            await _unitOfWork.PasswordResetTokenRepository.AddAsync(passwordResetToken, cancellationToken);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _emailNotificationService.SendResetPasswordTokenEmailAsync(user, token, cancellationToken);


            return true;
        }

        public async Task<bool> ResetPasswordAsync(
            string token,
            string password,
            string confirmPassword,
            CancellationToken cancellationToken = default)
        {

            var tokenHash = _tokenService.HashToken(token);

            var passwordResetToken = await _unitOfWork.PasswordResetTokenRepository.GetByTokenAsync(tokenHash, cancellationToken) ??
                throw new InvalidOperationException("El token no es válido o ha expirado");

            var user = await _unitOfWork.UserRepository.GetByIdAsync(passwordResetToken.IdUser, cancellationToken) ??
                throw new InvalidOperationException("El usuario no es válido");

            if (user.IdUserStatus != (short)UserStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("No es posible cambiar la contraseña para este usuario");
            }

            var salt = _hasherService.GenerateSalt();

            var hashedPassword = _hasherService.ComputeHashBytes(password, salt);

            user.Salt = salt;
            user.Password = hashedPassword;
            user.UpdatedAt = DateTime.UtcNow;

            passwordResetToken.UsedAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _emailNotificationService.SendResetPasswordEmailAsync(user, cancellationToken);

            return true;
        }
    }
}