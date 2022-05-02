using Microsoft.Extensions.Configuration;


namespace RssManager.AdministrationConsole.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        string? connectionString = configuration[name];
        if (connectionString == null)
            throw new ArgumentNullException(nameof(connectionString));
        return connectionString;
    }
}
