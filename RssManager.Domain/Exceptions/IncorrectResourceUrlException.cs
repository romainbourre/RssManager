namespace RssManager.Domain.Exceptions;

public class IncorrectResourceUrlException : Exception
{
    public IncorrectResourceUrlException() : base("url of resource is incorrect")
    {
    }
}
