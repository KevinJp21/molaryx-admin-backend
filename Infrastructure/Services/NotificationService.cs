using Application.Context;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Models.Notifications;
using Domain.Specifications;

namespace Infrastructure.Services
{
    public class NotificationService(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService,
        ICurrentUser _currentUser,
        INotificationRealtimePublisher _realtimePublisher
    ) : INotificationService
    {
        public async Task NotifyUserAsync(
            long idTenant,
            long idUser,
            string type,
            string subject,
            string body,
            CancellationToken cancellationToken = default)
        {
            var notification = new Notification
            {
                IdTenant = idTenant,
                IdUser = idUser,
                Type = type,
                Subject = subject,
                Body = body,
                IdNotificationStatus = (short)NotificationStatusEnum.UNREAD,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.NotificationRepository.AddAsync(notification, cancellationToken);
            await _unitOfWork.NotificationRepository.SaveChangesAsync(cancellationToken);

            await _realtimePublisher.PublishToUserAsync(
                idUser,
                new NotificationRealtimePayload
                {
                    IdNotification = notification.IdNotification,
                    Type = notification.Type,
                    Subject = notification.Subject,
                    Body = notification.Body,
                    IsViewed = false,
                    CreatedAt = notification.CreatedAt
                },
                cancellationToken);
        }

        public async Task<bool> MarkAsReadAsync(
            long idNotification,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idUser = _currentUser.IdUser!.Value;

            var notification = await _unitOfWork.NotificationRepository.GetFirstAsync(
                NotificationsSpec.ById(access.IdTenant, idUser, idNotification),
                cancellationToken)
                ?? throw new NotFoundException("La notificación no existe.");

            if (notification.IdNotificationStatus == (short)NotificationStatusEnum.READ)
            {
                return true;
            }

            notification.IdNotificationStatus = (short)NotificationStatusEnum.READ;
            notification.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.NotificationRepository.UpdateAsync(notification, cancellationToken);
            await _unitOfWork.NotificationRepository.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);
            var idUser = _currentUser.IdUser!.Value;

            var unread = await _unitOfWork.NotificationRepository.GetAll(
                NotificationsSpec.UnreadByUser(access.IdTenant, idUser),
                cancellationToken);

            if (unread is null || unread.Length == 0)
            {
                return true;
            }

            var now = DateTime.UtcNow;
            foreach (var notification in unread)
            {
                notification.IdNotificationStatus = (short)NotificationStatusEnum.READ;
                notification.UpdatedAt = now;
                await _unitOfWork.NotificationRepository.UpdateAsync(notification, cancellationToken);
            }

            await _unitOfWork.NotificationRepository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
