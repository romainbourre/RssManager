using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RssManager.AdministrationConsole.Core.CommandRouting;
using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;


namespace RssManager.AdministrationConsole;

internal static class Program
{
    public static async Task<int> Main(string[] args)
    {
        IConfigurationRoot? configuration = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var startup = new Startup();

        var serviceCollection = new ServiceCollection();
        startup.ConfigureServices(configuration, serviceCollection);
        IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        
        
        var routerBuilder = new CommandRouterBuilder(serviceProvider);
        startup.ConfigureRouting(routerBuilder);
        
        ICommand router = routerBuilder.Build();

        return await startup.Run(router, args);
    }
}
