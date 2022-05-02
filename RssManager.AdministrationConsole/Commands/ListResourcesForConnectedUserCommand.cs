using RssManager.AdministrationConsole.Core.CommandRouting.Interfaces;
using RssManager.AdministrationConsole.Interfaces;
using RssManager.Application.Interfaces;
using RssManager.Application.UseCases.GetResourcesForUser;
using RssManager.Domain.Entities;
using RssManager.AdministrationConsole.Extensions;
using RssManager.Application.Exceptions;


namespace RssManager.AdministrationConsole.Commands;

internal class ListResourcesForConnectedUserCommand : ICommand
{
    private readonly GetResourcesForUserUseCase useCase;
    private readonly IAuthenticationGateway authenticationGateway;
    private readonly IPresenter<GetResourcesForUserResponse> presenter;
    public ListResourcesForConnectedUserCommand(GetResourcesForUserUseCase useCase, IAuthenticationGateway authenticationGateway, IPresenter<GetResourcesForUserResponse> presenter)
    {
        this.useCase = useCase;
        this.authenticationGateway = authenticationGateway;
        this.presenter = presenter;
    }

    public async Task<int> Handle(Dictionary<string, string> @params, string[] args)
    {
        try
        {
            ConnectedUser connectedUser = this.GetConnectedUser();
            GetResourcesForUserResponse resources = await useCase.Handle(new GetResourcesForUserRequest(connectedUser.Id));
            this.presenter.Present(resources);
            return 0;
        }
        catch (UserNotFoundException e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }
    }

    private ConnectedUser GetConnectedUser()
    {
        return this.authenticationGateway.GetRequiredConnectedUser();
    }
}
