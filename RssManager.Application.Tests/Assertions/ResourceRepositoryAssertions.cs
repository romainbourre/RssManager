using FluentAssertions;
using RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;
using RssManager.Domain.Entities;


namespace RssManager.Application.Tests.Assertions;

public static class ResourceRepositoryAssertions
{

    public static void ShouldContainsResource(this InMemoryResourceRepository resourceRepository, Resource resource)
    {
        resourceRepository.Resources.Should().Contain(resource);
    }
}
