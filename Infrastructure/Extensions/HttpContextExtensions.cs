using System.Net;

namespace Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClientIpAddress(this HttpContext? httpContext)
        {
            if (httpContext is null)
                return null;

            var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(forwardedFor))
            {
                var firstIp = forwardedFor.Split(',')[0].Trim();
                if (IPAddress.TryParse(firstIp, out var forwardedIp))
                    return NormalizeIp(forwardedIp);
            }

            var realIp = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(realIp) && IPAddress.TryParse(realIp, out var parsedRealIp))
                return NormalizeIp(parsedRealIp);

            return httpContext.Connection.RemoteIpAddress is IPAddress remoteIp
                ? NormalizeIp(remoteIp)
                : null;
        }

        public static string? GetClientDevice(this HttpContext? httpContext)
        {
            if (httpContext is null)
                return null;

            var originalUserAgent = httpContext.Request.Headers["X-Client-User-Agent"].FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(originalUserAgent))
                return originalUserAgent;

            var userAgent = httpContext.Request.Headers.UserAgent.ToString();
            if (string.IsNullOrWhiteSpace(userAgent) || userAgent.Equals("node", StringComparison.OrdinalIgnoreCase))
                return null;

            return userAgent;
        }

        private static string NormalizeIp(IPAddress ip)
        {
            if (IPAddress.IsLoopback(ip))
                return IPAddress.Loopback.ToString();

            if (ip.IsIPv4MappedToIPv6)
                return ip.MapToIPv4().ToString();

            return ip.ToString();
        }
    }
}
