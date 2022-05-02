using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.AdministrationConsole.Gateways;

internal class AuthenticationGateway : IAuthenticationGateway
{
    private ConnectedUser? connectedUser;

    public ConnectedUser? ConnectedUser()
    {
        return this.connectedUser;
    }

    public AuthenticationGateway SetConnectedUser(ConnectedUser? user)
    {
        this.connectedUser = user;
        return this;
    }
}
