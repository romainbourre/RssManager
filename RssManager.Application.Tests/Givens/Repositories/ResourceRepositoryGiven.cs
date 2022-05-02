using System;
using RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;
using RssManager.Domain.Entities;
using RssManager.Domain.ValueObjects;


namespace RssManager.Application.Tests.Givens.Repositories;

public static class ResourceRepositoryGiven
{

    public static InMemoryResourceRepository AlreadyHasResource
    (
        this InMemoryResourceRepository resourceRepository,
        Guid ownerId,
        string title,
        string url,
        string description
    )
    {
        resourceRepository.Resources.Add(new Resource(
                Guid.NewGuid(),
                ownerId,
                Title.Of(title),
                Url.Of(url),
                description,
                DateTime.UtcNow
            )
        );

        return resourceRepository;
    }
}
