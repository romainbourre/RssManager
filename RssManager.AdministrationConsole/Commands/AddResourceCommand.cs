using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;
using RssManager.Application.UseCases.AddResourceForConnectedUser;


namespace RssManager.AdministrationConsole.Commands;

public class AddResourceCommand : ICommand
{
    private readonly AddResourceForConnectedUserUseCase useCase;
    public AddResourceCommand(AddResourceForConnectedUserUseCase useCase)
    {
        this.useCase = useCase;
    }

    public async Task<int> Handle(Dictionary<string, string> @params, string[] args)
    {
        var addResourceRequest = new AddResourceForConnectedUserRequest(
            args[0],
            args[1],
            args[2]
        );

        await this.useCase.Handle(addResourceRequest);

        return 0;
    }
}
