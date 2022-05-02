using RssManager.AdministrationConsole.Exceptions;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.AdministrationConsole.Extensions;

internal static class AuthenticationGatewayExtension
{
    public static ConnectedUser GetRequiredConnectedUser(this IAuthenticationGateway authenticationGateway)
    {
        ConnectedUser? connectedUser = authenticationGateway.ConnectedUser();
        if (connectedUser == null)
            throw new UserNotConnectedException();
        return connectedUser;
    }
}
