using System.IO.Compression;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shared.Common;

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
                        .SelectMany(x => x.Value!.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    var response = new ApiResponse<object>
                    {
                        Ok = false,
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