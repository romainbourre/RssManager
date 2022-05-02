namespace RssManager.Application.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid userId) : base($"user with id {userId} is not found")
    {
    }
}
