using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Services
{
    public class PasswordResetTokenService(
        IUnitOfWork _unitOfWork,
        ITokenService _tokenService,
        IEmailNotificationService _emailNotificationService
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

            var passwordResetToken = new PasswordResetToken
            {
                IdUser = user.IdUser,
                Token = token,
                ExpiresAt = currentDate.AddMinutes(15),
                CreatedAt = currentDate
            };

            await _unitOfWork.PasswordResetTokenRepository.AddAsync(passwordResetToken, cancellationToken);

            await _unitOfWork.SaveChangeAsync(cancellationToken);

            await _emailNotificationService.SendResetPasswordEmailAsync(user, token, cancellationToken);


            return true;
        }
    }
}