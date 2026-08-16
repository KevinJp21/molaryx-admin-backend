using Application.Features.Appointment.Command.CreateAppointment;
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
        ITenantAccessService _tenantAccessService
    ) : IAppointmentService
    {
        public async Task<bool> CreateAppointment(
            CreateAppointmentCommand request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idTenant = access.IdTenant;

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.IdUser, cancellationToken)
                ?? throw new NotFoundException("El usuario no existe.");

            if (user.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El usuario no pertenece a este consultorio.");
            }

            if (user.IdUserStatus != (short)UserStatusEnum.ACTIVE)
            {
                throw new InvalidOperationException("El usuario no está activo.");
            }

            var patient = await _unitOfWork.PatientsRepository.GetByIdAsync(
                    request.IdPatient,
                    cancellationToken,
                    PatientsSpec.ById(request.IdPatient))
                ?? throw new NotFoundException("El paciente no existe.");

            if (patient.IdTenant != idTenant)
            {
                throw new InvalidOperationException("El paciente no pertenece a este consultorio.");
            }

            var service = await _unitOfWork.ServiceRepository.GetByIdAsync(
                    request.IdService,
                    cancellationToken,
                    ServicesSpec.ById(request.IdService))
                ?? throw new NotFoundException("El servicio no existe.");

            if (service.IdTenant != idTenant || !service.IsActive)
            {
                throw new InvalidOperationException("El servicio no está disponible en este consultorio.");
            }

            var professional = await _unitOfWork.ProfessionalRepository.GetFirstAsync(
                ProfessionalSpec.ByUser(idTenant, user.IdUser),
                cancellationToken);

            if (professional is null && user.IdUserRole != (short)UserRoleEnum.OWNER)
            {
                throw new InvalidOperationException(
                    "El usuario seleccionado no es un profesional de este consultorio.");
            }

            try
            {
                if (professional is null)
                {
                    await _unitOfWork.BeginTransactionAsync(cancellationToken);

                    professional = new Professional
                    {
                        IdTenant = idTenant,
                        IdUser = user.IdUser
                    };

                    await _unitOfWork.ProfessionalRepository.AddAsync(professional, cancellationToken);
                    await _unitOfWork.ProfessionalRepository.SaveChangesAsync(cancellationToken);
                }

                var hasOverlap = await _unitOfWork.AppointmentRepository.GetFirstAsync(
                    AppointmentSpec.BySchedule(
                        idTenant,
                        professional.IdProfessional,
                        request.StartAt,
                        request.EndAt),
                    cancellationToken);

                if (hasOverlap is not null)
                {
                    throw new InvalidOperationException("El profesional ya tiene una cita en ese horario.");
                }

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
                IdAppointmentStatus = (short)AppointmentStatusEnum.PENDING,
                StartAt = request.StartAt,
                EndAt = request.EndAt,
                Notes = request.Notes
            };

            await _unitOfWork.AppointmentRepository.AddAsync(appointment, cancellationToken);
        }
    }
}
