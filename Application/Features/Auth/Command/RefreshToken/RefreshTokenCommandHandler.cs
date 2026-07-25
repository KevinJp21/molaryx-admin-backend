using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler(ISessionService sessionService) : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
    {
        private readonly ISessionService _sessionService = sessionService;

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var (authToken, refreshToken) =
                await _sessionService.RefreshSessionAsync(
                    request.RefreshToken,
                    cancellationToken
                );

            return new RefreshTokenCommandResponse
            {
                AuthToken = authToken,
                RefreshToken = refreshToken
            };
        }
    }
}