using Application.Common.Mediator.Interfaces;

namespace Application.Features.Auth.Command.RefreshToken
{
    public record RefreshTokenCommand : IRequest<RefreshTokenCommandResponse>;
}