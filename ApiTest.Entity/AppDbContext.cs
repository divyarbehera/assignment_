using ApiTest.Contracts.Database;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Entity;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        SetAuditableProperties();
        return await base.SaveChangesAsync(cancellationToken);
    }
    private void SetAuditableProperties()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created = DateTime.UtcNow;
                    entry.Entity.LastUpdated = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastUpdated = DateTime.UtcNow;
                    break;
            }
        }
    }


}


