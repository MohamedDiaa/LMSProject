using LMS.api.Configurations;
using NuGet.Configuration;
using RoboUnicornsLMS.Services;

namespace LMS.api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        //public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfigurationSection config)
        //{
        //    services.AddHttpClient(config);

        //    return services;
        //}

        public static IServiceCollection AddHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            var httpClientConfigs = configuration.GetSection("HttpClients").GetChildren() ??
                throw new InvalidOperationException("No configuration section 'HttpClients' found.");

            foreach (var config in httpClientConfigs)
            {
                var clientName = config.Key;
                var httpClientConfig = config.Get<HttpClientConfig>();

                if (httpClientConfig == null)
                {
                    throw new InvalidOperationException($"Configuration for HttpClient '{clientName}' not found.");
                }

                services.AddHttpClient(clientName, client =>
                {
                    client.BaseAddress = new Uri(httpClientConfig.BaseAddress ??
                        throw new InvalidOperationException("Base address not found in configuration."));

                    foreach (var header in httpClientConfig.DefaultRequestHeaders ??
                        throw new InvalidOperationException("Endpoint path not found in configuration."))
                    {
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                });
            }

            return services;
        }

        public static IServiceCollection AddRequestService<TService, TImplementation>(this IServiceCollection services, IConfiguration configuration)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            string serviceName = typeof(TService).Name;
            IConfigurationSection requestServiceConfig = configuration.GetSection($"RequestServices:{serviceName}");

            var baseAddress = requestServiceConfig.GetValue<string>("BaseAddress") ?? throw new InvalidOperationException($"Base address not found for service '{serviceName}'.");

            services.AddHttpClient<TService, TImplementation>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
                var defaultRequestHeaders = requestServiceConfig.GetSection("DefaultRequestHeaders").Get<Dictionary<string, string>>();
                foreach (var header in defaultRequestHeaders)
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }).Services.AddScoped<TService, TImplementation>(serviceProvider =>
            {
                var httpClient = serviceProvider.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(TService).Name);
                return Activator.CreateInstance(typeof(TImplementation), httpClient, configuration, serviceName) as TImplementation ?? throw new InvalidOperationException($"Could not create instance of {typeof(TImplementation).Name}.");
            });

            return services;
        }

#if false
        public static IServiceCollection AddRequestServices<TService, TImplementation>(this IServiceCollection services, IConfiguration configuration)
            where TService : class
            where TImplementation : class, TService
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            var requestServiceConfigs = configuration.GetSection("RequestServices").GetChildren() ??
                throw new InvalidOperationException("No configuration section 'RequestServices' found.");

            foreach (var config in requestServiceConfigs)
            {
                var serviceName = config.Key;
                var serviceConfig = config.Get<HttpClientConfig>();

                if (serviceConfig == null)
                {
                    throw new InvalidOperationException($"Configuration for service '{serviceName}' not found.");
                }

                services.AddScoped<TService, TImplementation>(service =>
                {
                    var httpClient = service.GetRequiredService<IHttpClientFactory>().CreateClient(serviceName);
                    return Activator.CreateInstance(typeof(TImplementation), httpClient, configuration) as TImplementation ??
                        throw new InvalidOperationException($"Could not create instance of {typeof(TImplementation).Name}.");
                });
            }

            return services;
        }

        public static IServiceCollection AddRequestServices(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

            var requestServiceConfigs = configuration.GetSection("RequestServices").GetChildren() ??
                throw new InvalidOperationException("No configuration section 'RequestServices' found.");

            foreach (var config in requestServiceConfigs)
            {
                var serviceName = config.Key;
                var serviceConfig = config.Get<HttpClientConfig>();

                if (serviceConfig == null)
                {
                    throw new InvalidOperationException($"Configuration for service '{serviceName}' not found.");
                }

                services.AddScoped(service =>
                {
                    var httpClient = service.GetRequiredService<IHttpClientFactory>().CreateClient(serviceName);
                    return Activator.CreateInstance(typeof(TImplementation), httpClient, configuration) as TImplementation ??
                        throw new InvalidOperationException($"Could not create instance of {typeof(TImplementation).Name}.");
                });
            }

            return services;
        }
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
            ApiSettings apiSettings = new();
            configuration.GetSection("ApiSettings").Bind(apiSettings);

            services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));

            services.AddHttpContextAccessor();
            services.AddTransient<AuthenticatedHttpClientHandler>();
            services.AddTransient<LoggingHandler>();

            foreach (var mapping in apiSettings.Mappings!)
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
#endif
    }
}
