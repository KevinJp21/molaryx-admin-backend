using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.Login
{
    public class loginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}