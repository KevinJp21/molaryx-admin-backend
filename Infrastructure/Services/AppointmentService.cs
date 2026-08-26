using Application.Features.Appointment.Command.CreateAppointment;
using Application.Features.Appointment.Command.UpdateAppointment;
using Application.Context;
using Domain.Common.Appointments;
using Domain.Common.Patients;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class AppointmentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService,
        INotificationHandler _notificationHandler,
        ICurrentUser _currentUser
    ) : IAppointmentService
    {
        public async Task<bool> CreateAppointment(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            await _tenantResourceService.RequirePatientAsync(idTenant, request.IdPatient, cancellationToken);
            await EnsureProceduresAsync(idTenant, request.Procedures, request.IdPatientTreatment, cancellationToken);

            if (request.IdPatientTreatment is > 0)
            {
                await EnsurePatientTreatmentAsync(idTenant, request.IdPatient, request.IdPatientTreatment.Value, cancellationToken);
            }

            try
            {
                var professional = await ResolveProfessionalAsync(
                    idTenant,
                    request.IdUser,
                    cancellationToken);

                await EnsureNoOverlapAsync(
                    idTenant,
                    professional.IdProfessional,
                    request.StartAt,
                    request.EndAt,
                    excludeIdAppointment: null,
                    cancellationToken);

                var appointment = await AddAppointmentAsync(
                    idTenant,
                    professional.IdProfessional,
                    request,
                    cancellationToken);

                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);
                }
                else
                {
                    await _unitOfWork.AppointmentRepository.SaveChangesAsync(cancellationToken);
                }

                if (_currentUser.IdUser != professional.IdUser)
                {
                    await _notificationHandler.NotifyAppointmentAssignedAsync(
                        idTenant,
                        professional.IdUser,
                        appointment.IdAppointment,
                        cancellationToken);
                }

                return true;
            }
            catch
            {
                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                }

                throw;
            }
        }

        public async Task<bool> UpdateAppointment(
            UpdateAppointmentCommand request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            var appointment = await _unitOfWork.AppointmentRepository.GetByIdAsync(
                request.IdAppointment,
                cancellationToken,
                AppointmentSpec.ByIdWithProcedures(request.IdAppointment))
                ?? throw new Domain.Exceptions.NotFoundException("La cita no existe.");

            if (appointment.IdTenant != idTenant)
            {
                throw new InvalidOperationException("La cita no pertenece a este consultorio.");
            }

            AppointmentStatusRules.EnsureCanEdit(appointment.IdAppointmentStatus);

            var idPatient = request.IdPatient ?? appointment.IdPatient;
            long? idPatientTreatment = appointment.IdPatientTreatment;
            if (request.IdPatientTreatment.HasValue)
            {
                idPatientTreatment = request.IdPatientTreatment.Value <= 0
                    ? null
                    : request.IdPatientTreatment;
            }

            var startAt = request.StartAt ?? appointment.StartAt;
            var endAt = request.EndAt ?? appointment.EndAt;
            var idAppointmentStatus = request.IdAppointmentStatus ?? appointment.IdAppointmentStatus;
            var notes = request.Notes ?? appointment.Notes;

            if (endAt <= startAt)
            {
                throw new InvalidOperationException("La hora de fin debe ser posterior a la de inicio.");
            }

            // Solo exigir horario futuro al reprogramar; permite actualizar estado/datos
            // de citas pasadas cuando StartAt/EndAt se reenvían sin cambio.
            if (request.StartAt.HasValue
                && request.StartAt.Value != appointment.StartAt
                && startAt <= DateTime.UtcNow)
            {
                throw new InvalidOperationException(
                    "La fecha y hora de inicio debe ser mayor a la fecha y hora actual.");
            }

            if (request.IdPatient.HasValue)
            {
                await _tenantResourceService.RequirePatientAsync(idTenant, idPatient, cancellationToken);
            }

            if (request.Procedures is not null)
            {
                await EnsureProceduresAsync(idTenant, request.Procedures, idPatientTreatment, cancellationToken);
            }
            else if (idPatientTreatment is not null
                && appointment.AppointmentProcedures.Any(p => p.Price != 0))
            {
                throw new InvalidOperationException(
                    "Los procedimientos de una cita con plan de tratamiento deben tener precio 0.");
            }
            else if (idPatientTreatment is null
                && appointment.AppointmentProcedures.Sum(p => p.Price) <= 0)
            {
                throw new InvalidOperationException(
                    "La cita debe tener al menos un procedimiento con precio mayor a 0.");
            }

            if (idPatientTreatment is not null)
            {
                await EnsurePatientTreatmentAsync(
                    idTenant,
                    idPatient,
                    idPatientTreatment.Value,
                    cancellationToken);
            }

            try
            {
                var previousIdProfessional = appointment.IdProfessional;
                long idProfessional = appointment.IdProfessional;
                long? newProfessionalIdUser = null;

                if (request.IdUser.HasValue)
                {
                    var professional = await ResolveProfessionalAsync(
                        idTenant,
                        request.IdUser.Value,
                        cancellationToken);
                    idProfessional = professional.IdProfessional;
                    newProfessionalIdUser = professional.IdUser;
                }

                var scheduleChanged =
                    idProfessional != appointment.IdProfessional
                    || startAt != appointment.StartAt
                    || endAt != appointment.EndAt;

                if (scheduleChanged)
                {
                    await EnsureNoOverlapAsync(
                        idTenant,
                        idProfessional,
                        startAt,
                        endAt,
                        appointment.IdAppointment,
                        cancellationToken);

                    if (startAt != appointment.StartAt)
                    {
                        appointment.ReminderSentAt = null;
                    }
                }

                appointment.IdPatient = idPatient;
                appointment.IdProfessional = idProfessional;
                appointment.IdPatientTreatment = idPatientTreatment;
                appointment.IdAppointmentStatus = idAppointmentStatus;
                appointment.StartAt = startAt;
                appointment.EndAt = endAt;
                appointment.Notes = notes;
                appointment.UpdatedAt = DateTime.UtcNow;

                if (request.Procedures is not null)
                {
                    appointment.AppointmentProcedures.Clear();
                    foreach (var item in request.Procedures)
                    {
                        appointment.AppointmentProcedures.Add(new AppointmentProcedure
                        {
                            IdProcedure = item.IdProcedure,
                            Price = NormalizePrice(idPatientTreatment, item.Price),
                            Notes = item.Notes,
                            CreatedAt = DateTime.UtcNow
                        });
                    }
                }
                else if (idPatientTreatment is not null)
                {
                    foreach (var item in appointment.AppointmentProcedures)
                    {
                        item.Price = 0;
                        item.UpdatedAt = DateTime.UtcNow;
                    }
                }

                await _unitOfWork.AppointmentRepository.UpdateAsync(appointment, cancellationToken);

                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);
                }
                else
                {
                    await _unitOfWork.AppointmentRepository.SaveChangesAsync(cancellationToken);
                }

                if (idProfessional != previousIdProfessional
                    && newProfessionalIdUser is long idUser
                    && _currentUser.IdUser != idUser)
                {
                    await _notificationHandler.NotifyAppointmentAssignedAsync(
                        idTenant,
                        idUser,
                        appointment.IdAppointment,
                        cancellationToken);
                }

                return true;
            }
            catch
            {
                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                }

                throw;
            }
        }

        private async Task EnsureProceduresAsync(
            long idTenant,
            List<AppointmentProcedureItem> procedures,
            long? idPatientTreatment,
            CancellationToken cancellationToken)
        {
            if (procedures.Count == 0)
            {
                throw new InvalidOperationException("Debe incluir al menos un procedimiento.");
            }

            if (procedures.Select(p => p.IdProcedure).Distinct().Count() != procedures.Count)
            {
                throw new InvalidOperationException("No se puede repetir el mismo procedimiento en la cita.");
            }

            if (idPatientTreatment is > 0)
            {
                if (procedures.Any(p => p.Price != 0))
                {
                    throw new InvalidOperationException(
                        "Los procedimientos de una cita con plan de tratamiento deben tener precio 0.");
                }
            }
            else if (procedures.Sum(p => p.Price) <= 0)
            {
                throw new InvalidOperationException(
                    "La cita debe tener al menos un procedimiento con precio mayor a 0.");
            }

            foreach (var item in procedures)
            {
                await _tenantResourceService.RequireProcedureAsync(
                    idTenant,
                    item.IdProcedure,
                    cancellationToken);
            }
        }

        private static decimal NormalizePrice(long? idPatientTreatment, decimal price) =>
            idPatientTreatment is > 0 ? 0 : price;

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
                throw new InvalidOperationException("El tratamiento no pertenece a este paciente.");
            }

            PatientTreatmentStatusRules.EnsureCanLinkAppointment(patientTreatment.IdPatientTreatmentStatus);
        }

        private async Task<Professional> ResolveProfessionalAsync(
            long idTenant,
            long idUser,
            CancellationToken cancellationToken)
        {
            var user = await _tenantResourceService.RequireUserAsync(idTenant, idUser, cancellationToken);

            if (user.IdUserStatus != (short)UserStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("El usuario no está activo.");
            }

            var professional = await _unitOfWork.ProfessionalRepository.GetFirstAsync(
                ProfessionalSpec.ByUser(idTenant, user.IdUser),
                cancellationToken);

            if (professional is not null)
            {
                return professional;
            }

            if (user.IdUserRole != (short)UserRoleEnum.OWNER)
            {
                throw new InvalidOperationException(
                    "El profesional seleccionado no está registrado en este consultorio.");
            }

            if (!_unitOfWork.IsInTransaction)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
            }

            professional = new Professional
            {
                IdTenant = idTenant,
                IdUser = user.IdUser
            };

            await _unitOfWork.ProfessionalRepository.AddAsync(professional, cancellationToken);
            await _unitOfWork.ProfessionalRepository.SaveChangesAsync(cancellationToken);

            return professional;
        }

        private async Task EnsureNoOverlapAsync(
            long idTenant,
            long idProfessional,
            DateTime startAt,
            DateTime endAt,
            long? excludeIdAppointment,
            CancellationToken cancellationToken)
        {
            var hasOverlap = await _unitOfWork.AppointmentRepository.GetFirstAsync(
                AppointmentSpec.BySchedule(
                    idTenant,
                    idProfessional,
                    startAt,
                    endAt,
                    excludeIdAppointment),
                cancellationToken);

            if (hasOverlap is not null)
            {
                throw new InvalidOperationException("El profesional ya tiene una cita en ese horario.");
            }
        }

        private async Task<Appointment> AddAppointmentAsync(
            long idTenant,
            long idProfessional,
            CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var idPatientTreatment = request.IdPatientTreatment is > 0
                ? request.IdPatientTreatment
                : null;

            var appointment = new Appointment
            {
                IdTenant = idTenant,
                IdPatient = request.IdPatient,
                IdProfessional = idProfessional,
                IdPatientTreatment = idPatientTreatment,
                IdAppointmentStatus = (short)AppointmentStatusEnum.PENDING,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                Notes = request.Notes,
                AppointmentProcedures = [.. request.Procedures.Select(item => new AppointmentProcedure
                {
                    IdProcedure = item.IdProcedure,
                    Price = NormalizePrice(idPatientTreatment, item.Price),
                    Notes = item.Notes,
                    CreatedAt = DateTime.UtcNow
                })]
            };

            await _unitOfWork.AppointmentRepository.AddAsync(appointment, cancellationToken);

            return appointment;
        }
    }
}
