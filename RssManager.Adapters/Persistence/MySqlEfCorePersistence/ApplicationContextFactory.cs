using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace RssManager.Adapters.Persistence.MySqlEfCorePersistence;

public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{

    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        const string connectionString = "server=localhost;user=root;password=root;database=RSS_MANAGER";
        ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
        optionsBuilder.UseMySql(connectionString, serverVersion);
        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
