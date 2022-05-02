using RssManager.Domain.Entities;


namespace RssManager.Application.Interfaces;

public interface IResourceRepository
{

    Task<IEnumerable<Resource>> GetResourcesForUser(Guid userId);
    Task Save(Resource resource);
}
