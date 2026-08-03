using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.ForgotPassword
{
    public class ForgotPasswordCommand : IRequest<bool>
    {
        public string Email { get; set; } = string.Empty;
    }
}