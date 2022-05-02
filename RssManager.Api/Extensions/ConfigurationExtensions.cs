namespace RssManager.Api.Extensions;

internal static class ConfigurationExtensions
{
    public static string GetRequiredConnectionString(this IConfiguration configuration, string name)
    {
        string? connectionString = configuration.GetConnectionString(name);
        if (connectionString == null)
            throw new ArgumentNullException(nameof(connectionString));
        return connectionString;
    }
}
