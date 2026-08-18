using Application.Features.Payment.Command.CreatePayment;
using Domain.Common;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class PaymentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IPaymentService
    {
        private static readonly TimeSpan PaidAtTolerance = TimeSpan.FromMinutes(5);

        public async Task<bool> CreatePaymentAsync(
            CreatePaymentCommand request,
            CancellationToken cancellationToken)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            var hasAppointment = request.IdAppointment.GetValueOrDefault() > 0;
            var hasPatientTreatment = request.IdPatientTreatment.GetValueOrDefault() > 0;

            if (hasAppointment == hasPatientTreatment)
            {
                throw new InvalidOperationException(
                    "El pago debe asociarse exactamente a una cita o a un tratamiento del paciente.");
            }

            if (request.Amount <= 0)
            {
                throw new InvalidOperationException("El monto del pago debe ser mayor a 0.");
            }

            EnsurePaidAt(request.PaidAt);

            await EnsurePaymentMethodAsync(request.IdPaymentMethod, cancellationToken);
            await EnsurePatientAsync(idTenant, request.IdPatient, cancellationToken);

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

        private static void EnsurePaidAt(DateTime paidAt)
        {
            if (paidAt == default)
            {
                throw new InvalidOperationException("La fecha de pago no es válida.");
            }

            if (paidAt > DateTime.UtcNow.Add(PaidAtTolerance))
            {
                throw new InvalidOperationException("La fecha de pago no puede ser futura.");
            }
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

        private async Task EnsurePatientAsync(
            long idTenant,
            long idPatient,
            CancellationToken cancellationToken)
        {
            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(
                    idPatient,
                    cancellationToken,
                    PatientsSpec.ById(idPatient))
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }
        }

        private async Task EnsureAppointmentAsync(
            long idTenant,
            long idPatient,
            long idAppointment,
            CancellationToken cancellationToken)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(
                    idAppointment,
                    cancellationToken,
                    AppointmentSpec.ById(idAppointment))
                ?? throw new NotFoundException("La cita no existe.");

            if (appointment.IdTenant != idTenant)
            {
                throw new InvalidOperationException("La cita no pertenece a este consultorio.");
            }

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
            var patientTreatment = await _unitOfWork.PatientTreatmentRepository.GetByIdAsync(
                    idPatientTreatment,
                    cancellationToken,
                    PatientTreatmentsSpec.ById(idPatientTreatment))
                ?? throw new NotFoundException("El tratamiento del paciente no existe.");

            if (patientTreatment.IdTenant != idTenant)
            {
                throw new InvalidOperationException(
                    "El tratamiento del paciente no pertenece a este consultorio.");
            }

            if (patientTreatment.IdPatient != idPatient)
            {
                throw new InvalidOperationException(
                    "El tratamiento no pertenece a este paciente.");
            }

            TreatmentStatusRules.EnsureCanReceivePayment(patientTreatment.IdTreatmentStatus);
        }
    }
}
