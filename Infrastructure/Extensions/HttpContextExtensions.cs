using System.Net;

namespace Infrastructure.Extensions
{
    public static class HttpContextExtensions
    {
        public static string? GetClientIpAddress(this HttpContext? httpContext)
        {
            if (httpContext?.Connection.RemoteIpAddress is not IPAddress remoteIp)
                return null;

            if (IPAddress.IsLoopback(remoteIp))
                return IPAddress.Loopback.ToString();

            if (remoteIp.IsIPv4MappedToIPv6)
                return remoteIp.MapToIPv4().ToString();

            return remoteIp.ToString();
        }
    }
}
