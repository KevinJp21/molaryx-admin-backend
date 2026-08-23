using Domain.Entities;

namespace Domain.Contracts.IServices
{
    public interface ITenantResourceService
    {
        Task<Patient> RequirePatientAsync(
            long idTenant,
            long idPatient,
            CancellationToken cancellationToken = default);

        Task<Appointment> RequireAppointmentAsync(
            long idTenant,
            long idAppointment,
            CancellationToken cancellationToken = default);

        Task<Procedure> RequireProcedureAsync(
            long idTenant,
            long idProcedure,
            CancellationToken cancellationToken = default);

        Task<Treatment> RequireTreatmentAsync(
            long idTenant,
            long idTreatment,
            CancellationToken cancellationToken = default);

        Task<PatientTreatment> RequirePatientTreatmentAsync(
            long idTenant,
            long idPatientTreatment,
            CancellationToken cancellationToken = default);

        Task<User> RequireUserAsync(
            long idTenant,
            long idUser,
            CancellationToken cancellationToken = default);
    }
}
