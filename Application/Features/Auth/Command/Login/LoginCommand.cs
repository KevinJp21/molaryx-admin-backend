using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;

namespace Application.Features.Auth.Command.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty!;
    }
}