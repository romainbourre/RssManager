using Microsoft.Extensions.DependencyInjection;
using RssManager.Adapters.Providers;
using RssManager.Application.Interfaces;


namespace RssManager.AdministrationConsole.Configurations;

internal static class ConfigureProvidersExtension
{
    public static IServiceCollection ConfigureProviders(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IDateTimeProvider, CurrentDateTimeProvider>()
            .AddScoped<IIdGenerator, GuidGenerator>();
        
        return serviceCollection;
    }
}
