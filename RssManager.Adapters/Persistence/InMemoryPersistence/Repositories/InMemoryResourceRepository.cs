using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;

public class InMemoryResourceRepository : IResourceRepository
{
    public readonly List<Resource> Resources;
    
    public InMemoryResourceRepository(List<Resource>? resources = null)
    {
        this.Resources = resources ?? new List<Resource>();
    }

    public Task<IEnumerable<Resource>> GetResourcesForUser(Guid userId)
    {
        return Task.FromResult(this.Resources.Where(resource => resource.OwnerId == userId));
    }
    
    public Task Save(Resource resource)
    {
        this.Resources.Add(resource);
        return Task.CompletedTask;
    }
}
