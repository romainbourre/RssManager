using System;
using RssManager.Adapters.Persistence.InMemoryPersistence.Repositories;
using RssManager.Domain.Entities;


namespace RssManager.Application.Tests.Givens.Repositories;

public static class UserRepositoryGiven
{

    public static InMemoryUserRepository AlreadyHasUser
    (
        this InMemoryUserRepository userRepository,
        Guid userId,
        string firstname,
        string lastname,
        string website,
        string description
    )
    {
        userRepository.Users.Add(new User(
                userId,
                firstname,
                lastname,
                website,
                description
            )
        );

        return userRepository;
    }
}
