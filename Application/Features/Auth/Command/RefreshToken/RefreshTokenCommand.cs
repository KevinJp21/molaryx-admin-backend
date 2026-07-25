using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenCommandResponse>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}