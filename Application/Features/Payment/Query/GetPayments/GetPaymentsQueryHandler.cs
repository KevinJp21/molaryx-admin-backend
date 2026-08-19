using Application.Common.Mediator.Interfaces;
using Application.Common.Pagination;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Payment.Query.GetPayments
{
    public class GetPaymentsQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetPaymentsQuery, PagedResult<GetPaymentsResponse>>
    {
        public async Task<PagedResult<GetPaymentsResponse>> Handle(
            GetPaymentsQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new PaymentsSpec(
                access.IdTenant,
                request.IdPatient,
                request.IdAppointment,
                request.IdPatientTreatment);

            var (totalItems, payments) = await _unitOfWork.PaymentRepository.GetPagedAsync(
                PaginationHelper.GetEffectivePage(request.Page),
                PaginationHelper.GetEffectivePageSize(request.Size),
                spec,
                cancellationToken
            );

            return new PagedResult<GetPaymentsResponse>
            {
                Items = [.. payments.Select(payment => new GetPaymentsResponse
                {
                    IdPayment = payment.IdPayment,
                    IdPatient = payment.IdPatient,
                    IdAppointment = payment.IdAppointment,
                    IdPatientTreatment = payment.IdPatientTreatment,
                    Amount = payment.Amount,
                    PaidAt = payment.PaidAt,
                    IdPaymentMethod = payment.IdPaymentMethod,
                    PaymentMethod = payment.PaymentMethod.Name,
                    Notes = payment.Notes,
                    Appointment = MapAppointment(payment.Appointment),
                    PatientTreatment = MapPatientTreatment(payment.PatientTreatment)
                })],
                Page = PaginationHelper.GetEffectivePage(request.Page),
                Size = PaginationHelper.GetEffectivePageSize(request.Size),
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling(
                    totalItems / (double)PaginationHelper.GetEffectivePageSize(request.Size)
                )
            };
        }

        private static PaymentAppointment? MapAppointment(
            Domain.Entities.Appointment? appointment)
        {
            if (appointment is null)
            {
                return null;
            }

            return new PaymentAppointment
            {
                IdAppointment = appointment.IdAppointment,
                IdService = appointment.IdService,
                ServiceName = appointment.Service.Name,
                IdAppointmentStatus = appointment.IdAppointmentStatus,
                AppointmentStatus = appointment.AppointmentStatus.Name,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                ProfessionalName = $"{appointment.Professional.User.FirstName}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondName) ? $" {appointment.Professional.User.SecondName}" : string.Empty)}",
                ProfessionalSurname = $"{appointment.Professional.User.FirstSurname}{(!string.IsNullOrEmpty(appointment.Professional.User.SecondSurname) ? $" {appointment.Professional.User.SecondSurname}" : string.Empty)}"
            };
        }

        private static PaymentPatientTreatment? MapPatientTreatment(
            Domain.Entities.PatientTreatment? patientTreatment)
        {
            if (patientTreatment is null)
            {
                return null;
            }

            return new PaymentPatientTreatment
            {
                IdPatientTreatment = patientTreatment.IdPatientTreatment,
                IdTreatment = patientTreatment.IdTreatment,
                TreatmentName = patientTreatment.Treatment.Name,
                AgreedPrice = patientTreatment.AgreedPrice,
                IdPatientTreatmentStatus = patientTreatment.IdPatientTreatmentStatus,
                PatientTreatmentStatus = patientTreatment.PatientTreatmentStatus.Name,
                StartAt = patientTreatment.StartAt,
                EndAt = patientTreatment.EndAt
            };
        }
    }
}
