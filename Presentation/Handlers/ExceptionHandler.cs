using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics;

namespace Presentation.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            ExceptionExtension.HandleException(
                exception,
                out var response
            );

            httpContext.Response.StatusCode =
                (int)response.HttpStatusCode;

            await httpContext.Response.WriteAsJsonAsync(
                response,
                cancellationToken
            );

            return true;
        }
    }
}