using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.RefreshToken
{
    public class RefreshTokenCommandHandler(ISessionService sessionService) : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
    {
        private readonly ISessionService _sessionService = sessionService;

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var (authToken, _) = await _sessionService.RefreshSessionAsync(
                cancellationToken
            );

            return new RefreshTokenCommandResponse
            {
                AuthToken = authToken
            };
        }
    }
}