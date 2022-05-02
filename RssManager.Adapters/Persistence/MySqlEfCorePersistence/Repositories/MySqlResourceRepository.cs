using Microsoft.EntityFrameworkCore;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Adapters.Persistence.MySqlEfCorePersistence.Repositories;

public class MySqlResourceRepository : IResourceRepository
{
    private readonly ApplicationDbContext context;
    public MySqlResourceRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    public async Task<IEnumerable<Resource>> GetResourcesForUser(Guid userId)
    {
        return await this.context.Resources
            .Where(resource => resource.OwnerId == userId)
            .ToListAsync();
    }
    public async Task Save(Resource resource)
    {
        this.context.Resources
            .Add(resource);
        await this.context.SaveChangesAsync();
    }
}
