using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RssManager.AdministrationConsole.Commands;
using RssManager.AdministrationConsole.Configurations;
using RssManager.AdministrationConsole.Core.CommandRouting.Exceptions;
using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;
using RssManager.AdministrationConsole.Extensions;


namespace RssManager.AdministrationConsole;

internal class Startup
{
    public void ConfigureServices(IConfiguration configuration, IServiceCollection serviceCollection)
    {
        string connectionString = configuration.GetRequiredConnectionString("CONNECTION_STRING");
        
        serviceCollection
            .UseMySqlPersistence(connectionString)
            .UseDeterministAuthentication()
            .ConfigureProviders()
            .ConfigureUseCases()
            .ConfigureCommands()
            .BuildServiceProvider();
    }
    
    public void ConfigureRouting(ICommandRouteBuilder routerBuilder)
    {
        routerBuilder
            .Add<ListResourceForUserCommand>("users/{userId}/resources")
            .AddGroup("resources", builder =>
            {
                builder
                    .Add<AddResourceCommand>("add")
                    .Add<ListResourcesForConnectedUserCommand>("list");
            });
    }

    public async Task<int> Run(ICommand router, string[] arguments)
    {
        try
        {
            var @params = new Dictionary<string, string>();
            return await router.Handle(@params, arguments);
        }
        catch (CommandNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }
}
