using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Npgsql;
using Infrastructure.Persistence.Repositories;
using Domain.Contracts.IServices;
using Infrastructure.Security;
using Infrastructure.Services;
using Domain.Contracts;
using Resend;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Shared.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Infrastructure.DependencyInjection;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "La cadena de conexión 'DefaultConnection' no está configurada."
            );
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(
                configuration.GetConnectionString("DefaultConnection")
            );

            var dataSource = dataSourceBuilder.Build();

            options.UseNpgsql(dataSource);
        });
        AddServices(services, configuration);
        AddRepositories(services);
        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        /*
             1. Registrar explícitamente la implementación de UnitOfWork.
             2. Obtener el ensamblado donde residen las implementaciones de repositorios
                (se toma el ensamblado que contiene a `UserRepository` como referencia).
             3. Buscar tipos concretos (clases no abstractas) cuyo nombre termine con "Repository".
             4. Para cada implementación encontrada, localizar la interfaz asociada cuyo nombre
                sea "I" + nombreDeLaClase (por ejemplo, UserRepository -> IUserRepository).
             5. Registrar cada pareja interfaz-implementación como Scoped.
             6. De este modo, cualquier repositorio nuevo añadido seguirá el patrón "I{Name}Repository"
                / "{Name}Repository" y será registrado automáticamente sin modificar este método.
            */

        // Registrar UnitOfWork explícitamente
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Registrar automáticamente repositorios que sigan el patrón "I{Name}Repository" / "{Name}Repository"

        var repoAssembly = typeof(UserRepository).Assembly;

        var repoTypes = repoAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Repository"));

        foreach (var impl in repoTypes)
        {
            var iface = impl.GetInterfaces().FirstOrDefault(i => i.Name == "I" + impl.Name);
            if (iface != null)
            {
                services.AddScoped(iface, impl);
            }
        }
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IHasherService, HasherService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<ITenantSubscriptionService, TenantSubscriptionService>();
        services.AddScoped<IPromotionService, PromotionService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddResend(options => options.ApiToken = configuration["Resend:ApiKey"]!);
        services.AddScoped<IEmailNotificationService, EmailNotificationService>();
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                await context.HttpContext.Response.WriteAsJsonAsync(
                    new ApiResponse<object>{
                        Ok = false,
                        Message = "Has realizado demasiadas solicitudes. Inténtalo nuevamente más tarde.",
                        Data= null
                    },
                    cancellationToken
                );
            };

            options.AddPolicy("auth", httpContext =>
            {
                var ipAddress =
                    httpContext.Connection.RemoteIpAddress?.ToString()
                    ?? "unknown";

                return RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: $"ip:{ipAddress}",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        QueueLimit = 0,
                        AutoReplenishment = true
                    }
                );
            });
        });
    }
}