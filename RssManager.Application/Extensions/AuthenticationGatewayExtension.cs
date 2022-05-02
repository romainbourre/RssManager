using RssManager.Application.Exceptions;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Application.Extensions;

internal static class AuthenticationGatewayExtension
{
    public static ConnectedUser ConnectedUserOrThrow(this IAuthenticationGateway authenticationGateway)
    {
        ConnectedUser? connectedUser = authenticationGateway.ConnectedUser();
        if (connectedUser == null)
            throw new AnyUserConnectedException();
        return connectedUser;
    }
}
