using Microsoft.Extensions.DependencyInjection;
using MyApp.Interfaces;
using MyApp.Services;
using MyApp.Repositories;

namespace MyApp.Api
{
    /// <summary>
    /// Central place to register application services, repositories, etc.
    /// </summary>
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Example registrations. Replace with real implementations as you build them.
            services.AddScoped<IHealthService, HealthService>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();

            // add other services/repositories here

            return services;
        }
    }
}