using Domain.Contracts;
using Domain.Contracts.IJobs;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Infrastructure.Jobs
{
    public class AppointmentReminderJob(
        IUnitOfWork _unitOfWork,
        INotificationHandler _notificationHandler
    ) : IAppointmentReminderJob
    {
        private static readonly TimeSpan ReminderWindow = TimeSpan.FromHours(1);

        public async Task<int> NotifyUpcomingAppointmentsAsync(
            CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var from = now;
            var to = now.Add(ReminderWindow);

            var appointments = await _unitOfWork.AppointmentRepository.GetAll(
                AppointmentSpec.ForUpcomingReminders(from, to),
                cancellationToken) ?? [];

            var notified = 0;

            foreach (var appointment in appointments)
            {
                if (appointment.Professional is null)
                {
                    continue;
                }

                await _notificationHandler.NotifyAppointmentReminderAsync(
                    appointment.IdTenant,
                    [appointment.Professional.IdUser],
                    appointment.IdAppointment,
                    appointment.StartAt,
                    FormatPatientName(appointment.Patient),
                    cancellationToken);

                appointment.ReminderSentAt = DateTime.UtcNow;
                appointment.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.AppointmentRepository.UpdateAsync(appointment, cancellationToken);
                await _unitOfWork.AppointmentRepository.SaveChangesAsync(cancellationToken);

                notified++;
            }

            return notified;
        }

        private static string FormatPatientName(Domain.Entities.Patient? patient)
        {
            if (patient is null)
            {
                return "paciente";
            }

            var parts = new[]
            {
                patient.FirstName,
                patient.SecondName,
                patient.FirstSurname,
                patient.SecondSurname
            }.Where(p => !string.IsNullOrWhiteSpace(p));

            var name = string.Join(' ', parts);
            return string.IsNullOrWhiteSpace(name) ? "paciente" : name;
        }
    }
}
