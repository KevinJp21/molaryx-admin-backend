using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;
using Shared.Utils;

namespace Presentation.Behaviors
{
    public static class ApiBehavior
    {
        public static IServiceCollection AddApiBehaviorConfiguration(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            x => string.IsNullOrEmpty(x.Key)
                                ? "request"
                                : x.Key.ToLowerEachProperty(),
                            x => x.Value!.Errors
                                .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                    ? "Valor inválido."
                                    : e.ErrorMessage)
                                .ToArray()
                        );

                    var response = new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Se encontraron errores de validación.",
                        Data = null,
                        Errors = errors,
                        HttpStatusCode = HttpStatusCode.BadRequest
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
