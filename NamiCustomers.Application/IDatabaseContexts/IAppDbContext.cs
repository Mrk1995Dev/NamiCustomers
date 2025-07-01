using NamiCustomers.Domain.Entities.Dealers;
using NamiCustomers.Domain.Entities.Subscribers;

namespace NamiCustomers.Application.IDatabaseContexts
{
    public interface IAppDbContext
    {
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<SubscriberCode> SubscriberCodes { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<City> Cities { get; set; }

        public int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
