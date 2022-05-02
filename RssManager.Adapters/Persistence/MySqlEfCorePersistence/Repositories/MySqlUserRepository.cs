using Microsoft.EntityFrameworkCore;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Adapters.Persistence.MySqlEfCorePersistence.Repositories;

public class MySqlUserRepository : IUserRepository
{
    private readonly ApplicationDbContext context;
    public MySqlUserRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<User?> GetById(Guid userId)
    {
        return await this.context.Users
            .SingleOrDefaultAsync(user => user.Id == userId);
    }
}
