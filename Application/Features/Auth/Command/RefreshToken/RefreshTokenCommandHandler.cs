using Application.Common.Mediator.Interfaces;
using Application.DTOs.Auth;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler(ISessionService sessionService) : IRequestHandler<RefreshTokenCommand, RefreshTokenResponseDto>
    {
        private readonly ISessionService _sessionService = sessionService;

        public async Task<RefreshTokenResponseDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var (authToken, refreshToken) =
                await _sessionService.RefreshSessionAsync(
                    request.RefreshToken,
                    cancellationToken
                );

            return new RefreshTokenResponseDto
            {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
        }
    }
}