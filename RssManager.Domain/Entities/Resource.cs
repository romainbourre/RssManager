using RssManager.Domain.ValueObjects;


namespace RssManager.Domain.Entities;

public record Resource(
    Guid Id,
    Guid OwnerId,
    Title Title,
    Url Url,
    string Description,
    DateTime CreatedAt
);
