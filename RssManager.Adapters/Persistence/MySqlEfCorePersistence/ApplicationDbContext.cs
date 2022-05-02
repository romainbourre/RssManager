using Microsoft.EntityFrameworkCore;
using RssManager.Domain.Entities;
using RssManager.Domain.ValueObjects;


namespace RssManager.Adapters.Persistence.MySqlEfCorePersistence;

public class ApplicationDbContext : DbContext
{
    
    public DbSet<Resource>? Resources { get; set; }
    public DbSet<User>? Users { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);
        
        modelBuilder.Entity<Resource>()
            .Property(resource => resource.Title)
            .HasConversion(title => title.ToString(), text => Title.Of(text));

        modelBuilder.Entity<Resource>()
            .Property(resource => resource.Url)
            .HasConversion(url => url.ToString(), text => Url.Of(text));

        modelBuilder.Entity<Resource>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(resource => resource.OwnerId);
    }
}
