using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Procedure.Command.UpdateProcedure
{
    public class UpdateProcedureCommandHandler(
        IProcedureService _procedureService
    ) : IRequestHandler<UpdateProcedureCommand, bool>
    {
        public Task<bool> Handle(UpdateProcedureCommand request, CancellationToken cancellationToken)
        {
            return _procedureService.UpdateProcedureAsync(request, cancellationToken);
        }
    }
}
