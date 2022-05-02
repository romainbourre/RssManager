using RssManager.AdministrationConsole.Core.CommandRouting.Exceptions;
using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;


namespace RssManager.AdministrationConsole.Core.CommandRouting;

internal class CommandGroup : ICommand
{
    private readonly IDictionary<string, ICommand> commands;
    
    public CommandGroup(IDictionary<string, ICommand>? commands = null)
    {
        this.commands = commands ?? new Dictionary<string, ICommand>();
    }

    public async Task<int> Handle(Dictionary<string, string> @params, string[] args)
    {
        foreach ((string? path, ICommand command) in this.commands)
        {
            string[] decomposedPath = path.Split("/");
            
            bool isCorrectPath = ResolvePath(decomposedPath, args, out Dictionary<string, string> pathParams);
            
            if (!isCorrectPath)
                continue;
            
            return await command.Handle(this.Merge(@params, pathParams), args[(decomposedPath.Length)..]);
        }

        throw new CommandNotFoundException(string.Join(" ", args));
    }

    private static bool ResolvePath(IReadOnlyList<string> path, IReadOnlyList<string> arguments, out Dictionary<string, string> @params)
    {
        @params = new Dictionary<string, string>();
        
        if (arguments.Count < path.Count)
            return false;
        
        for (var i = 0; i < path.Count; i++)
        {
            string pathSegment = path[i];
            string argumentsSegment = arguments[i];

            if (IsParameter(pathSegment))
            {
                @params[ComputeParameterName(pathSegment)] = argumentsSegment;
                continue;
            }

            if (argumentsSegment != pathSegment)
                return false;
        }
        
        return true;
    }
    
    private static bool IsParameter(string commandName)
    {
        return commandName.StartsWith("{") && commandName.EndsWith("}");
    }

    private static string ComputeParameterName(string pathSegment)
    {
        return pathSegment.Replace("{", string.Empty).Replace("}", string.Empty);
    }

    private Dictionary<string, string> Merge(Dictionary<string, string> first, Dictionary<string, string> second)
    {
        Dictionary<string, string> result = first.ToDictionary(element => element.Key, element => element.Value);

        foreach (KeyValuePair<string, string> element in second)
        {
            result.Add(element.Key, element.Value);
        }

        return result;
    }
}
