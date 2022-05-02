using RssManager.Application.Interfaces;


namespace RssManager.Adapters.Providers;

public class CurrentDateTimeProvider : IDateTimeProvider
{

    public DateTime Current()
    {
        return DateTime.UtcNow;
    }
}
