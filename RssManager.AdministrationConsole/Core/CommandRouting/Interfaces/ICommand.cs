namespace RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;

public interface ICommand
{
    Task<int> Handle(Dictionary<string, string> @params, string[] args);
}
