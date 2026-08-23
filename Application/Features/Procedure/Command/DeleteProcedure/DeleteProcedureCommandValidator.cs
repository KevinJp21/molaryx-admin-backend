using FluentValidation;

namespace Application.Features.Procedure.Command.DeleteProcedure
{
    public class DeleteProcedureCommandValidator : AbstractValidator<DeleteProcedureCommand>
    {
        public DeleteProcedureCommandValidator()
        {
            RuleFor(x => x.IdProcedure)
                .GreaterThan(0)
                .WithMessage("El procedimiento es obligatorio.");
        }
    }
}
