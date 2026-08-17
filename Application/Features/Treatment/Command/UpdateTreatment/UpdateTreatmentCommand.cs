using Application.Common.Mediator.Interfaces;

namespace Application.Features.Treatment.Command.UpdateTreatment
{
    public class UpdateTreatmentCommand : IRequest<bool>
    {
        public long IdTreatment { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
