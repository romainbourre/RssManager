namespace RssManager.AdministrationConsole.Core.CommandRouting.Exceptions;

internal class CommandNotFoundException : Exception
{
    public CommandNotFoundException(string command) : base($"command not found: \"{command}\"")
    {
    }
}
