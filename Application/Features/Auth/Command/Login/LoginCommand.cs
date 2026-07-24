using Application.Common.Mediator.Interfaces;
using Application.DTOs;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<LoginDTO>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty!;
    }
}