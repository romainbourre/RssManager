using RssManager.Domain.Entities;


namespace RssManager.Application.Interfaces;

public interface IUserRepository
{

    Task<User?> GetById(Guid userId);
}
