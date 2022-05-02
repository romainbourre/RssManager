namespace RssManager.Application.UseCases.AddResourceForConnectedUser;

public record AddResourceForConnectedUserRequest
(
    string ResourceTitle,
    string ResourceUrl,
    string ResourceDescription
);
