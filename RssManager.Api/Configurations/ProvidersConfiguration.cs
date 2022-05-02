using RssManager.Adapters.Providers;
using RssManager.Application.Interfaces;


namespace RssManager.Api.Configurations;

internal static class ProvidersConfiguration
{
    public static IServiceCollection AddProviders(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IDateTimeProvider, CurrentDateTimeProvider>();
        return serviceCollection;
    }
}
