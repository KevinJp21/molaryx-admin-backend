using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Procedure.Command.DeleteProcedure
{
    public class DeleteProcedureCommandHandler(
        IProcedureService _procedureService
    ) : IRequestHandler<DeleteProcedureCommand, bool>
    {
        public Task<bool> Handle(DeleteProcedureCommand request, CancellationToken cancellationToken)
        {
            return _procedureService.DeleteProcedureAsync(request.IdProcedure, cancellationToken);
        }
    }
}
