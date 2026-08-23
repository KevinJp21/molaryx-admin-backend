using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class TenantResourceService(IUnitOfWork _unitOfWork) : ITenantResourceService
    {
        public async Task<Patient> RequirePatientAsync(
            long idTenant,
            long idPatient,
            CancellationToken cancellationToken = default)
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

            return patient;
        }

        public async Task<Appointment> RequireAppointmentAsync(
            long idTenant,
            long idAppointment,
            CancellationToken cancellationToken = default)
        {
            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(
                    idAppointment,
                    cancellationToken,
                    AppointmentSpec.ByIdWithProcedures(idAppointment))
                ?? throw new NotFoundException("La cita no existe.");

            if (appointment.IdTenant != idTenant)
            {
                throw new InvalidOperationException("La cita no pertenece a este consultorio.");
            }

            return appointment;
        }

        public async Task<Procedure> RequireProcedureAsync(
            long idTenant,
            long idProcedure,
            CancellationToken cancellationToken = default)
        {
            var procedure = await _unitOfWork.ProcedureRepository.GetByIdAsync(
                    idProcedure,
                    cancellationToken,
                    ProceduresSpec.ById(idProcedure))
                ?? throw new NotFoundException("El procedimiento no existe.");

            if (procedure.IdTenant != idTenant || !procedure.IsActive)
            {
                throw new InvalidOperationException("El procedimiento no está disponible en este consultorio.");
            }

            return procedure;
        }

        public async Task<Treatment> RequireTreatmentAsync(
            long idTenant,
            long idTreatment,
            CancellationToken cancellationToken = default)
        {
            var treatment = await _unitOfWork.TreatmentRepository.GetByIdAsync(
                    idTreatment,
                    cancellationToken,
                    TreatmentsSpec.ById(idTreatment))
                ?? throw new NotFoundException("El tratamiento no existe.");

            if (treatment.IdTenant != idTenant || !treatment.IsActive)
            {
                throw new InvalidOperationException(
                    "El tratamiento no está disponible en este consultorio.");
            }

            return treatment;
        }

        public async Task<PatientTreatment> RequirePatientTreatmentAsync(
            long idTenant,
            long idPatientTreatment,
            CancellationToken cancellationToken = default)
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

            return patientTreatment;
        }

        public async Task<User> RequireUserAsync(
            long idTenant,
            long idUser,
            CancellationToken cancellationToken = default)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(idUser, cancellationToken)
                ?? throw new NotFoundException("El usuario no existe.");

            if (user.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El usuario no pertenece a este consultorio.");
            }

            return user;
        }
    }
}
