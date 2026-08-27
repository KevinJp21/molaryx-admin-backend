using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Enums;
using Domain.Specifications;
using Shared.Utils;

namespace Infrastructure.Handlers
{
    public class NotificationHandler(
        INotificationService _notificationService,
        IUnitOfWork _unitOfWork
    ) : INotificationHandler
    {
        public Task NotifyAppointmentAssignedAsync(
            long idTenant,
            long idUser,
            long idAppointment,
            CancellationToken cancellationToken = default)
        {
            var body = $"Se te ha asignado la cita #{idAppointment}.";

            return _notificationService.NotifyUserAsync(
                idTenant,
                idUser,
                nameof(NotificationTypeEnum.APPOINTMENT_ASSIGNED),
                "Cita asignada",
                body,
                cancellationToken);
        }

        public Task NotifyAppointmentReminderAsync(
            long idTenant,
            IReadOnlyCollection<long> idUsers,
            long idAppointment,
            DateTime startAt,
            string patientName,
            CancellationToken cancellationToken = default)
        {
            var startAtText = DateTimeHelper.ToColombiaTime(startAt).ToString("dd/MM/yyyy HH:mm");
            var body = $"Tienes la cita #{idAppointment} con {patientName} el {startAtText}.";

            return NotifyUsersAsync(
                idTenant,
                idUsers,
                nameof(NotificationTypeEnum.APPOINTMENT_REMINDER),
                "Recordatorio de cita",
                body,
                cancellationToken);
        }

        public async Task NotifyTenantRegisteredAsync(
            long idTenant,
            string consultoryName,
            CancellationToken cancellationToken = default)
        {
            var superAdmins = await _unitOfWork.UserRepository.GetAll(
                UserSpec.ForSuperAdmins(),
                cancellationToken) ?? [];

            if (superAdmins.Length == 0)
            {
                return;
            }

            var body =
                $"El consultorio \"{consultoryName}\" (#{idTenant}) se registró y está pendiente de aprobación.";

            await NotifyUsersAsync(
                idTenant: null,
                superAdmins.Select(u => u.IdUser).ToArray(),
                nameof(NotificationTypeEnum.TENANT_REGISTERED),
                "Nuevo consultorio registrado",
                body,
                cancellationToken);
        }

        private async Task NotifyUsersAsync(
            long? idTenant,
            IReadOnlyCollection<long> idUsers,
            string type,
            string subject,
            string body,
            CancellationToken cancellationToken)
        {
            if (idUsers.Count == 0)
            {
                return;
            }

            foreach (var idUser in idUsers.Distinct())
            {
                await _notificationService.NotifyUserAsync(
                    idTenant,
                    idUser,
                    type,
                    subject,
                    body,
                    cancellationToken);
            }
        }
    }
}
