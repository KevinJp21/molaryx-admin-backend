using Application.Features.Appointment.Command.CreateAppointment;
using Application.Features.Appointment.Command.UpdateAppointment;
using Domain.Common;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class AppointmentService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ITenantResourceService _tenantResourceService
    ) : IAppointmentService
    {
        public async Task<bool> CreateAppointment(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            await _tenantResourceService.RequirePatientAsync(idTenant, request.IdPatient, cancellationToken);
            await _tenantResourceService.RequireServiceAsync(idTenant, request.IdService, cancellationToken);

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

                await AddAppointmentAsync(
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

            var appointment = await _tenantResourceService.RequireAppointmentAsync(
                idTenant,
                request.IdAppointment,
                cancellationToken);

            AppointmentStatusRules.EnsureCanEdit(appointment.IdAppointmentStatus);

            var idPatient = request.IdPatient ?? appointment.IdPatient;
            var idService = request.IdService ?? appointment.IdService;
            long? idPatientTreatment = appointment.IdPatientTreatment;
            if (request.IdPatientTreatment.HasValue)
            {
                idPatientTreatment = request.IdPatientTreatment.Value <= 0
                    ? null
                    : request.IdPatientTreatment;
            }

            decimal? price = appointment.Price;
            if (idPatientTreatment is not null)
            {
                price = null;
            }
            else if (request.Price.HasValue)
            {
                price = request.Price.Value <= 0 ? null : request.Price;
            }
            var startAt = request.StartAt ?? appointment.StartAt;
            var endAt = request.EndAt ?? appointment.EndAt;
            var idAppointmentStatus = request.IdAppointmentStatus ?? appointment.IdAppointmentStatus;
            var notes = request.Notes ?? appointment.Notes;

            if (endAt <= startAt)
            {
                throw new InvalidOperationException("La hora de fin debe ser posterior a la de inicio.");
            }

            if (request.IdPatient.HasValue)
            {
                await _tenantResourceService.RequirePatientAsync(idTenant, idPatient, cancellationToken);
            }

            if (request.IdService.HasValue)
            {
                await _tenantResourceService.RequireServiceAsync(idTenant, idService, cancellationToken);
            }

            if (idPatientTreatment is not null)
            {
                await EnsurePatientTreatmentAsync(
                    idTenant,
                    idPatient,
                    idPatientTreatment.Value,
                    cancellationToken);
            }

            if (request.IdAppointmentStatus.HasValue)
            {
                AppointmentStatusRules.EnsureCanTransition(
                    appointment.IdAppointmentStatus,
                    request.IdAppointmentStatus.Value);
            }

            try
            {
                long idProfessional = appointment.IdProfessional;

                if (request.IdUser.HasValue)
                {
                    var professional = await ResolveProfessionalAsync(
                        idTenant,
                        request.IdUser.Value,
                        cancellationToken);
                    idProfessional = professional.IdProfessional;
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
                }

                appointment.IdPatient = idPatient;
                appointment.IdProfessional = idProfessional;
                appointment.IdService = idService;
                appointment.IdPatientTreatment = idPatientTreatment;
                appointment.Price = price;
                appointment.IdAppointmentStatus = idAppointmentStatus;
                appointment.StartAt = startAt;
                appointment.EndAt = endAt;
                appointment.Notes = notes;
                appointment.UpdatedAt = DateTime.UtcNow;

                await _unitOfWork.AppointmentRepository.UpdateAsync(appointment, cancellationToken);

                if (_unitOfWork.IsInTransaction)
                {
                    await _unitOfWork.CommitTransactionAsync(cancellationToken);
                }
                else
                {
                    await _unitOfWork.AppointmentRepository.SaveChangesAsync(cancellationToken);
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
                    "El usuario seleccionado no es un profesional de este consultorio.");
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

        private async Task AddAppointmentAsync(
            long idTenant,
            long idProfessional,
            CreateAppointmentCommand request,
            CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                IdTenant = idTenant,
                IdPatient = request.IdPatient,
                IdProfessional = idProfessional,
                IdService = request.IdService,
                IdPatientTreatment = request.IdPatientTreatment is > 0
                    ? request.IdPatientTreatment
                    : null,
                Price = request.IdPatientTreatment is > 0
                    ? null
                    : request.Price is > 0 ? request.Price : null,
                IdAppointmentStatus = (short)AppointmentStatusEnum.PENDING,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                Notes = request.Notes
            };

            await _unitOfWork.AppointmentRepository.AddAsync(appointment, cancellationToken);
        }
    }
}
