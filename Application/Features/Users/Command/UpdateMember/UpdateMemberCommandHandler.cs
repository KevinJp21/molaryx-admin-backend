using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Users.Command.UpdateMember
{
    public class UpdateMemberCommandHandler(
        IUserService _userService
    ) : IRequestHandler<UpdateMemberCommand, bool>
    {
        public Task<bool> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            return _userService.UpdateMemberAsync(request, cancellationToken);
        }
    }
}
