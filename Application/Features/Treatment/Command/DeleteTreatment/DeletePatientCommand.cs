using Application.Common.Mediator.Interfaces;

namespace Application.Features.Treatment.Command.DeleteTreatment
{
    public class DeleteTreatmentCommand : IRequest<bool>
    {
        public long IdTreatment { get; set; }
    }
}