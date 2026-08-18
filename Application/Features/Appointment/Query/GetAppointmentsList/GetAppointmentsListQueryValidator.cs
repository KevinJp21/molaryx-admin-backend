using Domain.Enums;
using FluentValidation;

namespace Application.Features.Appointment.Query.GetAppointmentsList
{
    public class GetAppointmentsListQueryValidator : AbstractValidator<GetAppointmentsListQuery>
    {
        public GetAppointmentsListQueryValidator()
        {
            When(x => x.IdPatient.HasValue, () =>
            {
                RuleFor(x => x.IdPatient)
                    .GreaterThan(0)
                    .WithMessage("El paciente no es válido.");
            });

            When(x => x.IdProfessional.HasValue, () =>
            {
                RuleFor(x => x.IdProfessional)
                    .GreaterThan(0)
                    .WithMessage("El profesional no es válido.");
            });

            When(x => x.IdAppointmentStatus.HasValue, () =>
            {
                RuleFor(x => x.IdAppointmentStatus)
                    .Must(status => Enum.IsDefined((AppointmentStatusEnum)status!.Value))
                    .WithMessage("El estado de la cita no es válido.");
            });
        }
    }
}
