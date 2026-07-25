using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Auth.Command.LogOut
{
    public class LogOutCommandHandler(ISessionService sessionService) : IRequestHandler<LogOutCommand, bool>
    {
        private readonly ISessionService _sessionService = sessionService;
        public async Task<bool> Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            await _sessionService.RevokeSessionAsync(request.RefreshToken, cancellationToken);

            return true;
        }
    }
}