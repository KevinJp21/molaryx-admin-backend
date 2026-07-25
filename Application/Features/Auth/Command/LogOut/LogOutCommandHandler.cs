using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.Logout
{
    public class LogoutCommandHandler(ISessionService sessionService) : IRequestHandler<LogoutCommand, bool>
    {
        private readonly ISessionService _sessionService = sessionService;
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _sessionService.RevokeSessionAsync(request.RefreshToken, cancellationToken);

            return true;
        }
    }
}