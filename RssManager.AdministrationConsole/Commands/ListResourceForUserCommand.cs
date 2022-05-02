using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;
using RssManager.AdministrationConsole.Interfaces;
using RssManager.Application.Exceptions;
using RssManager.Application.UseCases.GetResourcesForUser;


namespace RssManager.AdministrationConsole.Commands;

internal class ListResourceForUserCommand : ICommand
{
    private readonly GetResourcesForUserUseCase useCase;
    private readonly IPresenter<GetResourcesForUserResponse> presenter;
    public ListResourceForUserCommand(GetResourcesForUserUseCase useCase, IPresenter<GetResourcesForUserResponse> presenter)
    {
        this.useCase = useCase;
        this.presenter = presenter;
    }

    public async Task<int> Handle(Dictionary<string, string> @params, string[] args)
    {
        try
        {
            Guid userId = Guid.Parse(@params["userId"]);

            GetResourcesForUserResponse resources = await this.useCase
                .Handle(new GetResourcesForUserRequest(userId));

            this.presenter.Present(resources);

            return 0;
        }
        catch (UserNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }
}
