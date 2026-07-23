using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace WebAPI.Handlers
{
    public class ExceptionHandler : IExceptionHandler
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

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

            httpContext.Response.ContentType =
                "application/json; charset=utf-8";

            var jsonResponse = JsonSerializer.Serialize(
                response,
                JsonOptions
            );

            await httpContext.Response.WriteAsync(
                jsonResponse,
                cancellationToken
            );

            return true;
        }
    }
}