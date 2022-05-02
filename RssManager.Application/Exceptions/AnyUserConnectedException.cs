namespace RssManager.Application.Exceptions;

public class AnyUserConnectedException : Exception
{
    public AnyUserConnectedException() : base("any user is connected")
    {
    }
}
