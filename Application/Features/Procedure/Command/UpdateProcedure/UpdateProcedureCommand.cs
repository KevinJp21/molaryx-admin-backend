using Application.Common.Mediator.Interfaces;

namespace Application.Features.Procedure.Command.UpdateProcedure
{
    public class UpdateProcedureCommand : IRequest<bool>
    {
        public long IdProcedure { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? ReferencePrice { get; set; }
        public bool? IsActive { get; set; }
    }
}
