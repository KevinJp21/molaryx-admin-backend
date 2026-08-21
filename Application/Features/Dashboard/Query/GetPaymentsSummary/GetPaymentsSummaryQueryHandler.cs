using Application.Common.Mediator.Interfaces;
using Domain.Contracts;
using Domain.Contracts.IServices;
using Domain.Specifications;

namespace Application.Features.Dashboard.Query.GetPaymentsSummary
{
    public class GetPaymentsSummaryQueryHandler(
        IUnitOfWork _unitOfWork,
        ITenantAccessService _tenantAccessService
    ) : IRequestHandler<GetPaymentsSummaryQuery, GetPaymentsSummaryResponse>
    {
        private const int RevenueMonths = 6;

        public async Task<GetPaymentsSummaryResponse> Handle(
            GetPaymentsSummaryQuery request,
            CancellationToken cancellationToken = default)
        {
            var access = await _tenantAccessService.RequireActiveAsync(cancellationToken);

            var now = DateTime.UtcNow;
            var currentMonthStart = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var currentMonthEnd = currentMonthStart.AddMonths(1).AddTicks(-1);
            var seriesStart = currentMonthStart.AddMonths(-(RevenueMonths - 1));

            var payments = await _unitOfWork.PaymentRepository.GetAll(
                PaymentsSpec.ForDashboardSummary(access.IdTenant, seriesStart, currentMonthEnd),
                cancellationToken) ?? [];

            var currentMonthPayments = payments
                .Where(p => p.PaidAt >= currentMonthStart && p.PaidAt <= currentMonthEnd)
                .ToArray();

            return new GetPaymentsSummaryResponse
            {
                CurrentMonthRevenue = currentMonthPayments.Sum(p => p.Amount),
                RevenueOverTime = BuildRevenueOverTime(payments, seriesStart),
                PaymentMethods = BuildPaymentMethods(currentMonthPayments),
            };
        }

        private static List<RevenueOverTimeItem> BuildRevenueOverTime(
            Domain.Entities.Payment[] payments,
            DateTime seriesStart)
        {
            var byMonth = payments
                .GroupBy(p => new { p.PaidAt.Year, p.PaidAt.Month })
                .ToDictionary(
                    g => (g.Key.Year, g.Key.Month),
                    g => g.Sum(p => p.Amount));

            var items = new List<RevenueOverTimeItem>(RevenueMonths);
            for (var i = 0; i < RevenueMonths; i++)
            {
                var month = seriesStart.AddMonths(i);
                byMonth.TryGetValue((month.Year, month.Month), out var revenue);
                items.Add(new RevenueOverTimeItem
                {
                    Year = month.Year,
                    Month = month.Month,
                    Revenue = revenue,
                });
            }

            return items;
        }

        private static List<PaymentMethodShare> BuildPaymentMethods(
            Domain.Entities.Payment[] currentMonthPayments)
        {
            return [.. currentMonthPayments
                .GroupBy(p => new
                {
                    p.IdPaymentMethod,
                    Name = p.PaymentMethod.Name,
                })
                .OrderBy(g => g.Key.IdPaymentMethod)
                .Select(g => new PaymentMethodShare
                {
                    IdPaymentMethod = g.Key.IdPaymentMethod,
                    PaymentMethod = g.Key.Name,
                    Amount = g.Sum(p => p.Amount),
                    PaymentCount = g.Count(),
                })];
        }
    }
}
