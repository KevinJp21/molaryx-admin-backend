using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Npgsql;

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

        return services;
    }
}