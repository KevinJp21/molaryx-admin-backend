using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.Logout
{
    public class LogoutCommandHandler(ISessionService sessionService, ICurrentUser currentUser) : IRequestHandler<LogoutCommand, bool>
    {
        private readonly ISessionService _sessionService = sessionService;
             private readonly ICurrentUser _currentUser = currentUser;
        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _sessionService.RevokeSessionAsync(_currentUser.IdUser!.Value, request.RefreshToken, cancellationToken);

            return true;
        }
    }
}