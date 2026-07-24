using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty!;
    }
}