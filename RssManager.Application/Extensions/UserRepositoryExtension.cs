using RssManager.Application.Exceptions;
using RssManager.Application.Interfaces;
using RssManager.Domain.Entities;


namespace RssManager.Application.Extensions;

internal static class UserRepositoryExtension
{
    public static async Task<User> GetRequiredUserById(this IUserRepository userRepository, Guid userId)
    {
        User? user = await userRepository.GetById(userId);
        if (user == null)
            throw new UserNotFoundException(userId);
        return user;
    }
}
