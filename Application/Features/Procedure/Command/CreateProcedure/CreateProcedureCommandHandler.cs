using Application.Common.Mediator.Interfaces;
using Domain.Contracts.IServices;

namespace Application.Features.Procedure.Command.CreateProcedure
{
    public class CreateProcedureCommandHandler(
        IProcedureService _procedureService
    ) : IRequestHandler<CreateProcedureCommand, bool>
    {
        public Task<bool> Handle(CreateProcedureCommand request, CancellationToken cancellationToken)
        {
            return _procedureService.CreateProcedureAsync(request, cancellationToken);
        }
    }
}
