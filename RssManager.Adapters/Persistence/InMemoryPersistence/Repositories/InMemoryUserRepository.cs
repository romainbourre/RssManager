using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    public readonly List<User> Users;
    
    public InMemoryUserRepository(List<User>? users = null)
    {
        this.Users = users ?? new List<User>();
    }

    public Task<User?> GetById(Guid userId)
    {
        return Task.FromResult(this.Users.SingleOrDefault(user => user.Id == userId));
    }
}
