using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Users.Command.CreateMember
{
    public class CreateMemberCommandHandler(
        IUserService _userService,
        IEmailNotificationService _emailNotificationService,
        ILogger<CreateMemberCommandHandler> _logger
    ) : IRequestHandler<CreateMemberCommand, bool>
    {
        public async Task<bool> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var (success, temporaryPassword, consultoryName) =
                await _userService.CreateMemberAsync(request, cancellationToken);

            try
            {
                await _emailNotificationService.SendWelcomeMemberEmailAsync(
                    request.Email.Trim().ToLowerInvariant(),
                    request.FirstName,
                    temporaryPassword,
                    consultoryName,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error al enviar el correo de bienvenida al miembro {MemberEmail}.",
                    request.Email);
            }

            return success;
        }
    }
}
