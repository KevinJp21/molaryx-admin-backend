using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.LogOut
{
    public class LogOutCommand : IRequest<bool>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}