using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;

namespace Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponseDto>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}