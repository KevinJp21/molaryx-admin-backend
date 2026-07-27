using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Shared.Common;

namespace Infrastructure.Authorization
{
    public class PermissionAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public async Task HandleAsync(
            RequestDelegate next,
            HttpContext context,
            AuthorizationPolicy policy,
            PolicyAuthorizationResult authorizeResult)
        {
            if (authorizeResult.Forbidden)
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json ; charset=utf-8";

                var response = new ApiResponse<object>
                {
                    Ok = false,
                    Message = "No tienes permisos para realizar esta acción.",
                    Data = null
                };

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(
                        response,
                        JsonOptions
                    )
                );

                return;
            }

            await _defaultHandler.HandleAsync(
                next,
                context,
                policy,
                authorizeResult
            );
        }
    }
}