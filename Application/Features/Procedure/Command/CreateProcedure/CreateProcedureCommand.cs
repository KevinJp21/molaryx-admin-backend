using Application.Common.Mediator.Interfaces;

namespace Application.Features.Procedure.Command.CreateProcedure
{
    public class CreateProcedureCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal? ReferencePrice { get; set; }
    }
}
