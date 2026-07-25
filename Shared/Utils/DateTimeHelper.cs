namespace Shared.Utils
{
    public static class DateTimeHelper
    {
        private static readonly TimeZoneInfo ColombiaTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows()
                    ? "SA Pacific Standard Time"
                    : "America/Bogota"
            );

        public static DateTime ToColombiaTime(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc),
                ColombiaTimeZone
            );
        }
    }
}