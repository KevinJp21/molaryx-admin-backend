using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.Logout
{
    public class LogoutCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}