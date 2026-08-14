using Application.Common.Mediator.Interfaces;

namespace Application.Features.Service.Command.CreateService
{
    public class CreateServiceCommand : IRequest<bool>
    {
         public string Name { get; set; } = string.Empty;
         public string? Description { get; set; }
    }
}