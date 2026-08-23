using Application.Common.Mediator.Interfaces;

namespace Application.Features.Procedure.Command.DeleteProcedure
{
    public class DeleteProcedureCommand : IRequest<bool>
    {
        public long IdProcedure { get; set; }
    }
}
