using Microsoft.Extensions.DependencyInjection;
using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;


namespace RssManager.AdministrationConsole.Core.CommandRouting;

internal class CommandRouterBuilder : ICommandRouteBuilder
{
    private readonly IServiceProvider serviceProvider;
    private readonly IDictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
    public CommandRouterBuilder(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public ICommandRouteBuilder Add<T>(string path) where T : ICommand
    {
        this.commands.Add(path, this.serviceProvider.GetRequiredService<T>());
        return this;
    }

    public ICommandRouteBuilder AddGroup(string path, Action<CommandRouterBuilder> group)
    {
        var groupRouter = new CommandRouterBuilder(this.serviceProvider);
        group.Invoke(groupRouter);
        this.commands.Add(path, groupRouter.Build());
        return this;
    }
    
    public ICommand Build()
    {
        return new CommandGroup(this.commands);
    }
}
