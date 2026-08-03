using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.ResetPassword
{
    public class ResetPasswordCommandHandler(IPasswordResetTokenService _passwordResetTokenService) : IRequestHandler<ResetPasswordCommand, bool> {
        public async Task<bool> Handle(ResetPasswordCommand request, CancellationToken cancellationToken = default)
        {
            await _passwordResetTokenService.ResetPasswordAsync(request.Token, request.Password, request.ConfirmPassword, cancellationToken);

            return true;
        }
    }
}