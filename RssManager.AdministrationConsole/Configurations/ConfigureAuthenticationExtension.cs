using Microsoft.Extensions.DependencyInjection;
using RssManager.AdministrationConsole.Gateways;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.AdministrationConsole.Configurations;

internal static class ConfigureAuthenticationExtension
{
    public static IServiceCollection UseDeterministAuthentication(this IServiceCollection serviceCollection)
    {
        var connectedUser = new ConnectedUser(Guid.Parse("1bee9253-0655-40e7-8167-ca9d59e8df05"));
        
        AuthenticationGateway authenticationGateway = new AuthenticationGateway()
            .SetConnectedUser(connectedUser);
        
        serviceCollection
            .AddScoped<IAuthenticationGateway>(_ => authenticationGateway);

        return serviceCollection;
    }
}
