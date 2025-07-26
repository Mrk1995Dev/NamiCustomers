using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Subscribers;
using System.Data;
using System.Reflection;

namespace NamiCustomers.Persistence.DatabaseContexts;

public class AppDbContext : IdentityDbContext<Domain.Entities.Account.ApplicationUser,Domain.Entities.Account.ApplicationRole,string>, IAppDbContext
{
    public DbSet<VehicleAttachment> VehicleAttachments { get; set; }
    public DbSet<Subscriber> Subscribers { get; set; }
    public DbSet<SubscriberCode> SubscriberCodes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Domain.Entities.Account.ApplicationUser> Users { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<VehicleModel> VehicleModels { get; set; }

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
        // Seed initial roles
        SeedUserByRoles(modelBuilder);

       
    }

    private static void SeedUserByRoles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>().HasData(
                    new ApplicationRole
                    {
                        Id = "110alim-5841-4d44-b807-679d272e7110",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        Description = "ادمین"
                    },
                    new ApplicationRole { Id = "283875b0-1760-4e08-ba59-e532dc873bb7", Name = "Operator", NormalizedName = "OPERATOR", Description = "اپراتور" }
                );
        modelBuilder.Entity<ApplicationUser>().HasData(
           new ApplicationUser
           {
               Id = "1109abb4-7619-4567-9a1b-8dcf5e4b73aa",
               UserName = "a.moradi@namikhodro.com",
               NormalizedEmail = "A.MORADI@NAMIKHODRO.COM",
               EmailConfirmed = true,
               Email = "a.moardi@namikhodro.com",
               PassWord = "Aa12334566*",
               PasswordHash = "AQAAAAIAAYagAAAAEDWXR4EMPJhFFXowJTQ51DhTJ8/Trup0It8Ws2LzXKTf1sIhEMuKY3UFbYG/7uoq2A==",
               SecurityStamp = "VDH6RYMZDZ2U5JB5VYQRK47G6LZRQJ6O",
               ConcurrencyStamp = "7a0803d9-dac0-446f-b145-a48b202dbf52",
               FirstName = "علی",
               LastName = "مرادی",
               NormalizedUserName = "A.MORADI@NAMIKHODRO.COM",
               PhoneNumber = "09191646456",
               PhoneNumberConfirmed = true,

           });
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>() { RoleId = "110alim-5841-4d44-b807-679d272e7110", UserId = "1109abb4-7619-4567-9a1b-8dcf5e4b73aa" }

    );
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
