using RssManager.Application.Extensions;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Application.UseCases.GetResourcesForUser;

public class GetResourcesForUserUseCase : IUseCase<GetResourcesForUserRequest, GetResourcesForUserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly IResourceRepository resourceRepository;

    public GetResourcesForUserUseCase(IUserRepository userRepository, IResourceRepository resourceRepository)
    {
        this.userRepository = userRepository;
        this.resourceRepository = resourceRepository;
    }

    public async Task<GetResourcesForUserResponse> Handle(GetResourcesForUserRequest request)
    {
        User user = await FindUser(request);
        IEnumerable<Resource> resources = await this.FindResourcesForUser(user);
        return GetResponse(user, resources);
    }

    private async Task<User> FindUser(GetResourcesForUserRequest request)
    {
        return await this.userRepository
            .GetRequiredUserById(request.UserId);
    }

    private async Task<IEnumerable<Resource>> FindResourcesForUser(User user)
    {
        return await this.resourceRepository.GetResourcesForUser(user.Id);
    }

    private static GetResourcesForUserResponse GetResponse(User user, IEnumerable<Resource> resources)
    {
        return new GetResourcesForUserResponse(
            GetUserResponse(user),
            GetResourcesResponse(resources)
        );
    }

    private static GetResourcesForUserResponse.UserProfile GetUserResponse(User user)
    {
        return new GetResourcesForUserResponse.UserProfile(
            $"{user.Firstname} {user.Lastname}",
            user.Website,
            user.Description
        );
    }

    private static IEnumerable<GetResourcesForUserResponse.UserResource> GetResourcesResponse(IEnumerable<Resource> resources)
    {
        return resources.Select(resource => new GetResourcesForUserResponse.UserResource(
            resource.Title.ToString(),
            resource.Url.ToString(),
            resource.Description
        ));
    }
}
