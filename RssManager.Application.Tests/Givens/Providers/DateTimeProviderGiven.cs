using System;
using RssManager.Application.Tests.Providers;


namespace RssManager.Application.Tests.Givens.Providers;

public static class DateTimeProviderGiven
{

    public static void WillReturn(this DeterministDateTimeProvider dateTimeProvider, DateTime nextDateTime)
    {
        dateTimeProvider.SetDateTime(nextDateTime);
    }
}
