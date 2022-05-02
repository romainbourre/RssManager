using System;
using RssManager.Application.Tests.Gateways;
using RssManager.Domain.Entities;


namespace RssManager.Application.Tests.Givens.Gateways;

public static class AuthenticationGatewayGiven
{

    public static DeterministAuthenticationGateway HaveConnectedUser(this DeterministAuthenticationGateway  authenticationGateway, Guid connectedUserId)
    {
        authenticationGateway.SetConnectedUser(new ConnectedUser(connectedUserId));
        return authenticationGateway;
    }
}
