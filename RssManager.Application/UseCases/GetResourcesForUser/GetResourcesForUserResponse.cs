namespace RssManager.Application.UseCases.GetResourcesForUser;

public record GetResourcesForUserResponse
(
    GetResourcesForUserResponse.UserProfile User,
    IEnumerable<GetResourcesForUserResponse.UserResource> Resources
)
{
    public record UserProfile
    (
        string Fullname,
        string Website,
        string Description
    );

    public record UserResource
    (
        string Title,
        string Url,
        string Description
    );
}
