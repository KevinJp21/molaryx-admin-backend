
using FluentValidation;
using System.Reflection;
using Application.Behaviors;
using Application.Common.Mediator;
using Application.Common.Mediator.Interfaces;
using Application.Context;
using Infrastructure.Context;

namespace Application.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();


            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);

            var handlerInterface = typeof(IRequestHandler<,>);

            foreach (var type in assembly.GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface))
            {
                var interfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface);

                foreach (var @interface in interfaces)
                    services.AddScoped(@interface, type);
            }

            return services;
        }
    }
}