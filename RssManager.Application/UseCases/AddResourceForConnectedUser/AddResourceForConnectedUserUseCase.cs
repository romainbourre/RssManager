using RssManager.Application.Extensions;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;
using RssManager.Domain.ValueObjects;


namespace RssManager.Application.UseCases.AddResourceForConnectedUser;

public class AddResourceForConnectedUserUseCase : IUseCase<AddResourceForConnectedUserRequest, AddResourceForConnectedUserResponse>
{
    private readonly IResourceRepository resourceRepository;
    private readonly IAuthenticationGateway authenticationGateway;
    private readonly IIdGenerator idGenerator;
    private readonly IDateTimeProvider dateTimeProvider;

    public AddResourceForConnectedUserUseCase
    (
        IResourceRepository resourceRepository,
        IAuthenticationGateway authenticationGateway,
        IIdGenerator idGenerator,
        IDateTimeProvider dateTimeProvider
    )
    {
        this.resourceRepository = resourceRepository;
        this.authenticationGateway = authenticationGateway;
        this.idGenerator = idGenerator;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<AddResourceForConnectedUserResponse> Handle(AddResourceForConnectedUserRequest request)
    {
        ConnectedUser connectedUser = this.GetConnectedUser();
        Resource resourceToSave = CreateResourceForUser(connectedUser, request);
        Resource savedResource = await this.SaveResource(resourceToSave);
        return GetResponse(savedResource);
    }

    private ConnectedUser GetConnectedUser()
    {
        return this.authenticationGateway.ConnectedUserOrThrow();
    }

    private Resource CreateResourceForUser(ConnectedUser user, AddResourceForConnectedUserRequest request)
    {
        return new Resource(
            this.idGenerator.GetNext(),
            user.Id,
            Title.Of(request.ResourceTitle),
            Url.Of(request.ResourceUrl),
            request.ResourceDescription,
            this.dateTimeProvider.Current()
        );
    }

    private async Task<Resource> SaveResource(Resource resourceToSave)
    {
        await this.resourceRepository.Save(resourceToSave);
        return resourceToSave;
    }

    private static AddResourceForConnectedUserResponse GetResponse(Resource savedResource)
    {
        return new AddResourceForConnectedUserResponse(
            savedResource.Id,
            savedResource.Title.ToString(),
            savedResource.Url.ToString()
        );
    }
}
