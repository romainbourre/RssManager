using RssManager.Application.Interfaces;


namespace RssManager.Adapters.Providers;

public class GuidGenerator : IIdGenerator
{

    public Guid GetNext()
    {
        return Guid.NewGuid();
    }
}
