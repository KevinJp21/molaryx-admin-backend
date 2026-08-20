using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Models;
using Domain.Specifications;
using Shared.Utils;

namespace Application.Features.Payment.Query.GetPaymentReport
{
    public class GetPaymentReportQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        IPaymentReportService _paymentReportService
    ) : IRequestHandler<GetPaymentReportQuery, PaymentReportFileResult>
    {
        public async Task<PaymentReportFileResult> Handle(
            GetPaymentReportQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var spec = new PaymentsSpec(
                access.IdTenant,
                request.IdPatient,
                request.IdAppointment,
                request.IdPatientTreatment,
                request.From,
                request.To);

            var payments = await _unitOfWork.PaymentRepository.GetAll(spec, cancellationToken)
                ?? [];

            var reportInformation = new PaymentReportInformation
            {
                ConsultoryName = access.Tenant.ConsultoryName,
                GeneratedAt = DateTimeHelper.ToColombiaTime(DateTime.UtcNow),
                PeriodFrom = request.From.HasValue
                    ? DateTimeHelper.ToColombiaTime(request.From.Value)
                    : null,
                PeriodTo = request.To.HasValue
                    ? DateTimeHelper.ToColombiaTime(request.To.Value)
                    : null,
                Rows = [.. payments.Select(MapRow)],
            };

            return new PaymentReportFileResult
            {
                Content = _paymentReportService.GeneratePaymentReport(reportInformation),
                FileName = BuildFileName(access.Tenant.ConsultoryName),
            };
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
                ServiceName = payment.Appointment?.Service.Name ?? "—",
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
