using Application.Common.Mediator.Interfaces;

namespace Application.Features.Treatment.Command.CreateTreatment
{
    public class CreateTreatmentCommand : IRequest<bool>
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
