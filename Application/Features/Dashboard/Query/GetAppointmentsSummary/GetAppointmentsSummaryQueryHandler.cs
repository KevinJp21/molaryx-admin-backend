using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;
using AppointmentEntity = Domain.Entities.Appointment;

namespace Application.Features.Dashboard.Query.GetAppointmentsSummary
{
    public class GetAppointmentsSummaryQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetAppointmentsSummaryQuery, GetAppointmentsSummaryResponse>
    {
        private const int TopLimit = 5;
        private const int UpcomingLimit = 8;

        public async Task<GetAppointmentsSummaryResponse> Handle(
            GetAppointmentsSummaryQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var now = DateTime.UtcNow;
            var todayStart = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);
            var todayEnd = todayStart.AddDays(1).AddTicks(-1);
            var monthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var monthEnd = monthStart.AddMonths(1).AddTicks(-1);

            var monthAppointments = await _unitOfWork.AppointmentRepository.GetAll(
                AppointmentSpec.ForDashboardPeriod(access.IdTenant, monthStart, monthEnd),
                cancellationToken) ?? [];

            var (_, upcomingAppointments) = await _unitOfWork.AppointmentRepository.GetPagedAsync(
                1,
                UpcomingLimit,
                AppointmentSpec.ForDashboardUpcoming(access.IdTenant, now),
                cancellationToken);

            return new GetAppointmentsSummaryResponse
            {
                TodayCount = monthAppointments.Count(a =>
                    a.StartAt >= todayStart && a.StartAt <= todayEnd),
                ByStatus = BuildByStatus(monthAppointments),
                TopProcedures = BuildTopProcedures(monthAppointments),
                Upcoming = [.. upcomingAppointments.Select(MapUpcoming)],
            };
        }

        private static List<AppointmentStatusShare> BuildByStatus(AppointmentEntity[] appointments)
        {
            return [.. appointments
                .GroupBy(a => new
                {
                    a.IdAppointmentStatus,
                    Name = a.AppointmentStatus.Name,
                })
                .OrderBy(g => g.Key.IdAppointmentStatus)
                .Select(g => new AppointmentStatusShare
                {
                    IdAppointmentStatus = g.Key.IdAppointmentStatus,
                    AppointmentStatus = g.Key.Name,
                    Count = g.Count(),
                })];
        }

        private static List<TopProcedureItem> BuildTopProcedures(AppointmentEntity[] appointments)
        {
            return [.. appointments
                .SelectMany(a => a.AppointmentProcedures)
                .GroupBy(p => new { p.IdProcedure, p.Procedure.Name })
                .OrderByDescending(g => g.Count())
                .ThenBy(g => g.Key.Name)
                .Take(TopLimit)
                .Select(g => new TopProcedureItem
                {
                    IdProcedure = g.Key.IdProcedure,
                    ProcedureName = g.Key.Name,
                    Count = g.Count(),
                })];
        }

        private static UpcomingAppointmentItem MapUpcoming(AppointmentEntity appointment)
        {
            return new UpcomingAppointmentItem
            {
                IdAppointment = appointment.IdAppointment,
                StartAt = appointment.StartAt,
                EndAt = appointment.EndAt,
                PatientName = BuildPersonName(
                    appointment.Patient.FirstName,
                    appointment.Patient.SecondName),
                PatientSurname = BuildPersonName(
                    appointment.Patient.FirstSurname,
                    appointment.Patient.SecondSurname),
                ProcedureNames = string.Join(
                    ", ",
                    appointment.AppointmentProcedures.Select(p => p.Procedure.Name)),
                ProfessionalName = BuildPersonName(
                    appointment.Professional.User.FirstName,
                    appointment.Professional.User.SecondName),
                ProfessionalSurname = BuildPersonName(
                    appointment.Professional.User.FirstSurname,
                    appointment.Professional.User.SecondSurname),
                IdAppointmentStatus = appointment.IdAppointmentStatus,
                AppointmentStatus = appointment.AppointmentStatus.Name,
            };
        }

        private static string BuildPersonName(string first, string? second)
            => $"{first}{(!string.IsNullOrEmpty(second) ? $" {second}" : string.Empty)}";
    }
}
