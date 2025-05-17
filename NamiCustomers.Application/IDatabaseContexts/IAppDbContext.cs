namespace NamiCustomers.Application.IDatabaseContexts
{
    public interface IAppDbContext
    {
        public DbSet<SAMPLEEntity> SAMPLEs { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<City> Cities { get; set; }

        public int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
