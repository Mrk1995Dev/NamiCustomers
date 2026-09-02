using NamiCustomers.Application.Seeds;
using NamiCustomers.Domain.Entities.Dealers;

namespace NamiCustomers.Application.Configs;

public class DealerConfig : IEntityTypeConfiguration<Dealer>
{
    public void Configure(EntityTypeBuilder<Dealer> builder)
    {
        builder.ToTable("Dealers");
        builder.Property(d => d.DealerNo).HasMaxLength(20);
        builder.Property(d => d.DealerName).IsRequired().HasMaxLength(150);
        builder.Property(d => d.ManagerName).HasMaxLength(100);
        builder.Property(d => d.DealerAddress).HasMaxLength(500);
        builder.Property(d => d.DealerPrePhone).HasMaxLength(10);
        builder.Property(d => d.DealerPhone).HasMaxLength(150);
        builder.Property(d => d.DealerMobile).HasMaxLength(15);
        builder.Property(d => d.Fax).HasMaxLength(20);
        builder.Property(d => d.Email).HasMaxLength(100);
        builder.Property(d => d.PostalCode).HasMaxLength(10);
        builder.Property(d => d.NationalId).HasMaxLength(11);
        builder.Property(d => d.EconomicCode).HasMaxLength(20);
        builder.Property(d => d.Description).HasMaxLength(500);
        builder.Property(d => d.IsActive).HasDefaultValue(true);
        builder.HasIndex(d => d.DealerNo).IsUnique().HasFilter("[DealerNo] IS NOT NULL");
        builder.HasOne(d => d.City)
            .WithMany()
            .HasForeignKey(d => d.CityId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasData(IranDealerSeed.Dealers);
    }
}
