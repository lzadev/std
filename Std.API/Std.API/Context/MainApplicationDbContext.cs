using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;
using Std.API.Models;

namespace Std.API.Context;

public class MainApplicationDbContext(DbContextOptions<MainApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SystemParameter> SystemParameters { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
    }

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

public class BlankTriggerAddingConvention : IModelFinalizingConvention
{
    public virtual void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
            if (table != null
                && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null)
                && (entityType.BaseType == null
                    || entityType.GetMappingStrategy() != RelationalAnnotationNames.TphMappingStrategy))
            {
                entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
            }

            foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
            {
                if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                {
                    entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                }
            }
        }
    }
}