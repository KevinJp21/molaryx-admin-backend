using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.ResetPassword
{
    public class ResetPasswordCommand : IRequest<bool>
    {
        public string Token { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}