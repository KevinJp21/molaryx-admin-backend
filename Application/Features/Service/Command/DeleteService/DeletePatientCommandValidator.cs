using FluentValidation;

namespace Application.Features.Service.Command.DeleteService
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator()
        {
            RuleFor(x => x.IdService)
                .GreaterThan(0)
                .WithMessage("El servicio es obligatorio.");
        }
    }
}
