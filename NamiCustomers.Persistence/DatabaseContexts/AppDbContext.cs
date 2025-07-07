using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NamiCustomers.Domain.Entities.Dealers;
using NamiCustomers.Domain.Entities.Subscribers;
using System.Reflection;

namespace NamiCustomers.Persistence.DatabaseContexts;

public class AppDbContext : IdentityDbContext<Domain.Entities.Account.ApplicationUser,Domain.Entities.Account.ApplicationRole,string>, IAppDbContext
{
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<SubscriberCode> SubscriberCodes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Domain.Entities.Account.ApplicationUser> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }
    public DbSet<Dealer> Dealers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
          .AddInterceptors(new SoftDeleteInterceptor());

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType
                .GetCustomAttributes(
                typeof(AuditableAttribute), true).Length > 0)
            {
                modelBuilder.Entity(entityType.Name).Property<DateTime>("CreateAt");
                modelBuilder.Entity(entityType.Name).Property<DateTime?>("LastModifiedAt");
                modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemovedAt");
                modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");


                // New soft delete filter check
                //if (entityType.ClrType.GetProperty("IsRemoved") != null)
                //{
                //    var parameter = Expression.Parameter(entityType.ClrType, "b");
                //    var property = Expression.Property(parameter, "IsRemoved");
                //    var notExpression = Expression.Not(property);
                //    var lambda = Expression.Lambda(notExpression, parameter);

                //    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                //}
                 modelBuilder.Entity<Subscriber>().HasQueryFilter(b => !EF.Property<bool>(b, "IsRemoved"));
                 modelBuilder.Entity<VehicleModel>().HasQueryFilter(b => !EF.Property<bool>(b, "IsRemoved"));
            }

            
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(SubscriberConfig)));
        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
          c.State == EntityState.Added ||
          c.State == EntityState.Deleted).ToList();
        foreach (var item in candidas)
        {
            var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
            var inserted = entityType.FindProperty("CreateAt");
            var modified = entityType.FindProperty("LastModifiedAt");
            var deleted = entityType.FindProperty("RemovedAt");
            var isRemoved = entityType.FindProperty("IsRemoved");
            if (item.State == EntityState.Added && inserted is not null)
            {
                item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
            }
            if (item.State == EntityState.Modified && modified is not null)
            {
                item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
            }
            if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
            {
                item.Property("IsRemoved").CurrentValue = true;
                item.Property("RemovedAt").CurrentValue = DateTime.UtcNow;
                item.State = EntityState.Modified;
            }
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var candidas = ChangeTracker.Entries().Where(c => c.State == EntityState.Modified ||
          c.State == EntityState.Added ||
          c.State == EntityState.Deleted).ToList();
        foreach (var item in candidas)
        {
            var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());
            var inserted = entityType.FindProperty("CreateAt");
            var modified = entityType.FindProperty("LastModifiedAt");
            var deleted = entityType.FindProperty("RemovedAt");
            var isRemoved = entityType.FindProperty("IsRemoved");
            if (item.State == EntityState.Added && inserted is not null)
            {
                item.Property("CreateAt").CurrentValue = DateTime.UtcNow;
            }
            if (item.State == EntityState.Modified && modified is not null)
            {
                item.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
            }
            if (item.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
            {
                item.Property("IsRemoved").CurrentValue = true;
                item.Property("RemovedAt").CurrentValue = DateTime.UtcNow;

                item.State = EntityState.Modified;
            }
        }

        return await base.SaveChangesAsync();
    }


}
