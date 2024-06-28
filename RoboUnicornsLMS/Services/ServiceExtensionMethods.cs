using RoboUnicornsLMS.Services;
namespace RoboUnicornsLMS.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRequestService<TService, TImplementation>(this IServiceCollection services)
        where TService : class
        where TImplementation : class, TService
    {
        services.AddScoped<TService, TImplementation>();
        services.AddHttpClient<TService, TImplementation>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:7183");
        });

        return services;
    }

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
            var entityType = Type.GetType(mapping.Key.Name);
            if (entityType == null)
            {
                throw new InvalidOperationException($"Type '{mapping.Key}' not found.");
            }

            var entityName = mapping.Value;
            //var serviceType = typeof(IGenericRequestService<>).MakeGenericType(entityType);
            //var implementationType = typeof(GenericRequestService<>).MakeGenericType(entityType);

            var httpClientName = $"{entityType.Name}Client";

            services.AddHttpClient(httpClientName, client =>
            {
                client.BaseAddress = new Uri($"{apiSettings.Host}/{apiSettings.EndPoint}/{entityName}");
            })
            .AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
            .AddHttpMessageHandler<LoggingHandler>();

            //services.AddTransient(serviceType, serviceProvider =>
            //{
            //    var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
            //    var httpClient = httpClientFactory.CreateClient(httpClientName);
            //    return Activator.CreateInstance(implementationType, httpClient);
            //});
        }

        return services;
    }
}
public static class ServiceCollectionExtensions2
{
    public static IServiceCollection AddGenericServices2(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();

        foreach (var mapping in apiSettings.Mappings)
        {
            var serviceType = typeof(IGenericRequestService<,>).MakeGenericType(mapping.Key);
            var implementationType = typeof(GenericRequestService<,>).MakeGenericType(mapping.Key);
            services.AddScoped(serviceType, provider =>
            {
                var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient();
                httpClient.BaseAddress = new Uri(apiSettings.Host);
                return Activator.CreateInstance(implementationType, new object[] { httpClient, mapping.Value });
            });
        }

        return services;
    }
}
