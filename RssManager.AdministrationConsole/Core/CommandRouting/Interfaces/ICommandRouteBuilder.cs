namespace RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;

internal interface ICommandRouteBuilder
{
    public ICommandRouteBuilder Add<T>(string path) where T : ICommand;
    public ICommandRouteBuilder AddGroup(string path, Action<CommandRouterBuilder> group);
    public ICommand Build();
}
