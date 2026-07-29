using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Common;
using Shared.Options;

namespace Presentation.Config.Jwt
{
    public static class JwtAuthenticationExtensions
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("Jwt").Get<JwtOptions>()
                ?? throw new InvalidOperationException("La configuración Jwt no está configurada.");

            if (string.IsNullOrWhiteSpace(jwtOptions.Key))
            {
                throw new InvalidOperationException(
                    "Jwt:Key no configurado"
                );
            }


            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        jwtOptions.Key
                                    )
                            ),

                            ValidateIssuer = true,
                            ValidIssuer = jwtOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = jwtOptions.Audience,

                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                        };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode =
                                StatusCodes.Status401Unauthorized;

                            context.Response.ContentType =
                                "application/json; charset=utf-8";

                            var response = new ApiResponse<object>
                            {
                                Ok = false,
                                Message =
                                    "Usuario no autenticado.",
                                Data = null
                            };

                            return context.Response.WriteAsync(
                                JsonSerializer.Serialize(
                                    response,
                                    JsonOptions
                                )
                            );
                        }
                    };
                });

            return services;
        }
    }
}