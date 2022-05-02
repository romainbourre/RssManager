namespace RssManager.Domain.Entities;

public record User(
    Guid Id,
    string Firstname,
    string Lastname,
    string Website,
    string Description
);
