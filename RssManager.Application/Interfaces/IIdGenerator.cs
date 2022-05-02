namespace RssManager.Application.Interfaces;

public interface IIdGenerator
{

    Guid GetNext();
}
