using Microsoft.Extensions.DependencyInjection;
using RssManager.Application.UseCases.AddResourceForConnectedUser;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.AdministrationConsole.Configurations;

internal static class ConfigureUseCasesExtension
{
    public static IServiceCollection ConfigureUseCases(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<GetResourcesForUserUseCase>()
            .AddScoped<AddResourceForConnectedUserUseCase>()
            .AddScoped<GetResourcesForUserUseCase>();

        return serviceCollection;
    }
}
