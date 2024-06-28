using LMS.api.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;

namespace RoboUnicornsLMS.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericServices(this IServiceCollection services, IConfiguration configuration)
        {
            var apiSettings = new ApiSettings();
            configuration.GetSection("ApiSettings").Bind(apiSettings);

            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddHttpContextAccessor();
            services.AddTransient<AuthenticatedHttpClientHandler>();
            services.AddTransient<LoggingHandler>();

            foreach (var mapping in apiSettings.Mappings)
            {
                var entityType = mapping.Key;
                var entityName = mapping.Value;
                var serviceType = typeof(IGenericService<>).MakeGenericType(entityType);
                var implementationType = typeof(GenericService<>).MakeGenericType(entityType);

                var httpClientName = $"{entityType.Name}Client".ToLower();

                services.AddHttpClient(httpClientName, client =>
                {
                    client.BaseAddress = new Uri($"{apiSettings.Host.ToLower()}/{apiSettings.EndPoint.ToLower()}/{entityName.ToLower()}");
                })
                .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
                .AddHttpMessageHandler<LoggingHandler>();

                services.AddTransient(serviceType, serviceProvider =>
                {
                    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
                    var httpClient = httpClientFactory.CreateClient(httpClientName);
                    return Activator.CreateInstance(implementationType, httpClient);
                });
            }

            return services;
        }
    }
}
