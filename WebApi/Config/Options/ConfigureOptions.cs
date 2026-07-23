using Shared.Options;

namespace WebAPI.Config.Options
{
    public static class ConfigureOptions
    {
        public static IServiceCollection AddConfiguredOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(
                configuration.GetSection("Jwt"));
            return services;
        }
    }
}
