using Application.Common.Mediator.Interfaces;
using Application.Context;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.LogoutAll
{
    public class LogoutAllCommandHandler(ISessionService sessionService, ICurrentUser currentUser) : IRequestHandler<LogoutAllCommand, bool>
    {
        private readonly ISessionService _sessionService = sessionService;
        private readonly ICurrentUser _currentUser = currentUser;

        public async Task<bool> Handle(LogoutAllCommand request, CancellationToken cancellationToken){
            var userId = _currentUser.IdUser!.Value;

            await _sessionService.RevokeAllSessionAsync(userId, cancellationToken);

            return true;
        }
    }
}