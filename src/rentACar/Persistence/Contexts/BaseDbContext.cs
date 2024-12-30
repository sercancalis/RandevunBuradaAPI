using System.Reflection;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<BusinessImage> BusinessImages { get; set; }
    public DbSet<WorkingHour> WorkingHours { get; set; }
    public DbSet<BusinessService> BusinessServices { get; set; }
    public DbSet<UserDevice> UserDevices { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public BaseDbContext(IConfiguration config)
    {
        Configuration = config;
    }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration)
        : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        IEnumerable<EntityEntry<Entity>> entries = ChangeTracker
            .Entries<Entity>()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        //foreach (EntityEntry<Entity> entry in entries)
        //    _ = entry.State switch
        //    {
        //        EntityState.Added => entry.Entity.CreatedDate = DateTime.UtcNow,
        //        EntityState.Modified => entry.Entity.UpdatedDate = DateTime.UtcNow
        //    };

        foreach (EntityEntry<Entity> entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.IsActive = true;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedDate = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
