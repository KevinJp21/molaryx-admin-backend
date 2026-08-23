using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Models;
using Domain.Specifications;
using Shared.Utils;

namespace Application.Features.Payment.Query.GetPaymentsReport
{
    public class GetPaymentsReportQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        IPaymentReportService _paymentReportService
    ) : IRequestHandler<GetPaymentsReportQuery, (byte[] Content, string FileName)>
    {
        public async Task<(byte[] Content, string FileName)> Handle(
            GetPaymentsReportQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var paidFrom = NormalizeFrom(request.From);
            var paidToInclusive = NormalizeToInclusive(request.To);

            var spec = new PaymentsSpec(
                access.IdTenant,
                request.IdPatient,
                request.IdAppointment,
                request.IdPatientTreatment,
                paidFrom,
                paidToInclusive);

            var payments = await _unitOfWork.PaymentRepository.GetAll(spec, cancellationToken)
                ?? [];

            var reportInformation = new PaymentReportInformation
            {
                ConsultoryName = access.Tenant.ConsultoryName,
                GeneratedAt = DateTimeHelper.ToColombiaTime(DateTime.UtcNow),
                PeriodFrom = request.From,
                PeriodTo = request.To,
                Rows = [.. payments.Select(MapRow)],
            };

            return (
                _paymentReportService.GeneratePaymentReport(reportInformation),
                BuildFileName(access.Tenant.ConsultoryName));
        }

        private static DateTime? NormalizeFrom(DateOnly? from)
        {
            return from.HasValue
                ? from.Value.ToDateTime(TimeOnly.MinValue)
                : null;
        }

        private static DateTime? NormalizeToInclusive(DateOnly? to)
        {
            return to.HasValue
                ? to.Value.ToDateTime(TimeOnly.MinValue).AddDays(1).AddTicks(-1)
                : null;
        }

        private static PaymentReportRowInformation MapRow(Domain.Entities.Payment payment)
        {
            var isAppointmentPayment = payment.IdAppointment is not null;

            return new PaymentReportRowInformation
            {
                PaidAt = DateTimeHelper.ToColombiaTime(payment.PaidAt),
                PatientFullName = FormatPatientFullName(payment.Patient),
                IdentificationTypeCode = payment.Patient.IdentificationType.Code.Trim(),
                IdentificationNumber = payment.Patient.IdentificationNumber.Trim(),
                Concept = isAppointmentPayment ? "Cita" : "Tratamiento",
                TreatmentName = payment.PatientTreatment?.Treatment.Name ?? "—",
                ProcedureNames = payment.Appointment is null
                    ? "—"
                    : string.Join(
                        ", ",
                        payment.Appointment.AppointmentProcedures.Select(p => p.Procedure.Name)),
                PaymentMethod = payment.PaymentMethod.Name,
                Amount = payment.Amount,
                Notes = string.IsNullOrWhiteSpace(payment.Notes) ? "—" : payment.Notes.Trim(),
            };
        }

        private static string FormatPatientFullName(Domain.Entities.Patient patient)
        {
            var name = $"{patient.FirstName}{FormatOptionalName(patient.SecondName)}";
            var surname = $"{patient.FirstSurname}{FormatOptionalName(patient.SecondSurname)}";

            return $"{name} {surname}".Trim();
        }

        private static string FormatOptionalName(string? value)
        {
            return !string.IsNullOrEmpty(value) ? $" {value}" : string.Empty;
        }

        private static string BuildFileName(string consultoryName)
        {
            var slug = new string(
                consultoryName
                    .Trim()
                    .ToLowerInvariant()
                    .Select(c => char.IsLetterOrDigit(c) ? c : '-')
                    .ToArray())
                .Trim('-');

            if (string.IsNullOrWhiteSpace(slug))
            {
                slug = "consultorio";
            }

            return $"reporte-pagos-{slug}-{DateTime.UtcNow:yyyyMMdd}.xlsx";
        }
    }
}
