namespace RssManager.Application.UseCases.AddResourceForConnectedUser;

public record AddResourceForConnectedUserResponse
(
    Guid ResourceId,
    string ResourceTitle,
    string ResourceUrl
);
