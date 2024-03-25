using Microsoft.EntityFrameworkCore;
using Std.API.Models;

namespace Std.API.Context;

public class BackUpApplicationContext(DbContextOptions<BackUpApplicationContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseModelWithAudit>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Deleted = false;
                    entry.Entity.Active = true;
                    entry.Entity.CreationTime = DateTimeOffset.Now;
                    entry.Entity.UpdateTime = null;
                    entry.Entity.DeletionTime = null;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateTime = DateTimeOffset.Now;
                    break;

            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseModelWithAudit>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Deleted = false;
                    entry.Entity.Active = true;
                    entry.Entity.CreationTime = DateTimeOffset.Now;
                    entry.Entity.UpdateTime = null;
                    entry.Entity.DeletionTime = null;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdateTime = DateTimeOffset.Now;
                    break;

            }
        }

        return base.SaveChanges();
    }
}