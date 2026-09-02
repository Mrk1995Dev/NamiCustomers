using NamiCustomers.Domain.Entities.Subscribers;

namespace NamiCustomers.Application.Configs;

public class SubscriberConfig : IEntityTypeConfiguration<Subscriber>
{
    public void Configure(EntityTypeBuilder<Subscriber> builder)
    {
        builder.HasIndex(c => c.NationalCode).IsUnique();
    }
}