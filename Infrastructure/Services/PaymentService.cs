using Application.Features.Payment.Command.CreatePayment;
using Domain.Common;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;

namespace Infrastructure.Services
{
    public class PaymentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService
    ) : IPaymentService
    {
        public async Task<bool> CreatePaymentAsync(
            CreatePaymentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            var hasAppointment = request.IdAppointment.GetValueOrDefault() > 0;
            var hasPatientTreatment = request.IdPatientTreatment.GetValueOrDefault() > 0;

            await EnsurePaymentMethodAsync(request.IdPaymentMethod, cancellationToken);
            await _tenantResourceService.RequirePatientAsync(idTenant, request.IdPatient, cancellationToken);

            if (hasAppointment)
            {
                await EnsureAppointmentAsync(
                    idTenant,
                    request.IdPatient,
                    request.IdAppointment!.Value,
                    cancellationToken);
            }
            else
            {
                await EnsurePatientTreatmentAsync(
                    idTenant,
                    request.IdPatient,
                    request.IdPatientTreatment!.Value,
                    cancellationToken);
            }

            var payment = new Payment
            {
                IdTenant = idTenant,
                IdPatient = request.IdPatient,
                IdAppointment = hasAppointment ? request.IdAppointment : null,
                IdPatientTreatment = hasPatientTreatment ? request.IdPatientTreatment : null,
                Amount = request.Amount,
                PaidAt = request.PaidAt,
                IdPaymentMethod = request.IdPaymentMethod,
                Notes = string.IsNullOrWhiteSpace(request.Notes)
                    ? null
                    : request.Notes.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.PaymentRepository.AddAsync(payment, cancellationToken);
            await _unitOfWork.PaymentRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task EnsurePaymentMethodAsync(
            short idPaymentMethod,
            CancellationToken cancellationToken)
        {
            var paymentMethod = await _unitOfWork.PaymentMethodRepository.GetByIdAsync(
                    idPaymentMethod,
                    cancellationToken)
                ?? throw new NotFoundException("El método de pago no existe.");

            if (!paymentMethod.IsActive)
            {
                throw new InvalidOperationException(
                    $"El método de pago {paymentMethod.Name} no está disponible.");
            }
        }

        private async Task EnsureAppointmentAsync(
            long idTenant,
            long idPatient,
            long idAppointment,
            CancellationToken cancellationToken)
        {
            var appointment = await _tenantResourceService.RequireAppointmentAsync(
                idTenant,
                idAppointment,
                cancellationToken);

            if (appointment.IdPatient != idPatient)
            {
                throw new InvalidOperationException("La cita no pertenece a este paciente.");
            }

            AppointmentStatusRules.EnsureCanReceivePayment(appointment.IdAppointmentStatus);
        }

        private async Task EnsurePatientTreatmentAsync(
            long idTenant,
            long idPatient,
            long idPatientTreatment,
            CancellationToken cancellationToken)
        {
            var patientTreatment = await _tenantResourceService.RequirePatientTreatmentAsync(
                idTenant,
                idPatientTreatment,
                cancellationToken);

            if (patientTreatment.IdPatient != idPatient)
            {
                throw new InvalidOperationException(
                    "El tratamiento no pertenece a este paciente.");
            }

            PatientTreatmentStatusRules.EnsureCanReceivePayment(patientTreatment.IdPatientTreatmentStatus);
        }
    }
}
