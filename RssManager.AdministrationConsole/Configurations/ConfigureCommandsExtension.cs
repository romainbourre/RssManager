using Microsoft.Extensions.DependencyInjection;
using RssManager.AdministrationConsole.Commands;
using RssManager.AdministrationConsole.Interfaces;
using RssManager.AdministrationConsole.Views;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.AdministrationConsole.Configurations;

internal static class ConfigureCommandsExtension
{
    public static IServiceCollection ConfigureCommands(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<AddResourceCommand>()
            .AddScoped<ListResourceForUserCommand>()
            .AddScoped<ListResourcesForConnectedUserCommand>();

        serviceCollection
            .AddScoped<IPresenter<GetResourcesForUserResponse>, ResourceListView>();
        
        return serviceCollection;
    }
}
