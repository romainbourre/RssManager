using System;
using RssManager.Application.Interfaces;


namespace RssManager.Application.Tests.Providers;

public class DeterministDateTimeProvider : IDateTimeProvider
{
    private DateTime computedDateTime;

    public void SetDateTime(DateTime nextDateTime)
    {
        this.computedDateTime = nextDateTime;
    }
    public DateTime Current()
    {
        return this.computedDateTime;
    }
}
