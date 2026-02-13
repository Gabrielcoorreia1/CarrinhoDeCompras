using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PurchasingSystem.Application.Behavios;

namespace PurchasingSystem.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            
            services.AddValidatorsFromAssembly(assembly);
            
            return services;
        }
    }
}
