using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Application.Tests.Gateways;

public class DeterministAuthenticationGateway : IAuthenticationGateway
{
    private ConnectedUser? connectedUser;
    
    public ConnectedUser? ConnectedUser()
    {
        return this.connectedUser;
    }
    
    public void SetConnectedUser(ConnectedUser user)
    {
        this.connectedUser = user;
    }
}
