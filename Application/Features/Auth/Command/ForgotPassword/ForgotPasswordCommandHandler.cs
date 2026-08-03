using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommandHandler(IPasswordResetTokenService _passwordResetTokenService) : IRequestHandler<ForgotPasswordCommand, bool>
    {
        public async Task<bool> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken = default)
        {
            return await _passwordResetTokenService.SendResetPasswordTokenAsync(request.Email, cancellationToken);
        }
    }
}