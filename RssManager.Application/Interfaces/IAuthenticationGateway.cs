using RssManager.Domain.Entities;


namespace RssManager.Application.Interfaces;

public interface IAuthenticationGateway
{

    ConnectedUser? ConnectedUser();
}
