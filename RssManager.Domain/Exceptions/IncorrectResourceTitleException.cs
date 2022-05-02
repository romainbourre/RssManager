namespace RssManager.Domain.Exceptions;

public class IncorrectResourceTitleException : Exception
{
    public IncorrectResourceTitleException() : base("resource cannot have empty title")
    {
    }
}
