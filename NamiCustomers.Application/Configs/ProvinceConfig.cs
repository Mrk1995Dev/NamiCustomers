using NamiCustomers.Application.Seeds;

namespace NamiCustomers.Application.Configs;

public class ProvinceConfig : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.ToTable("Provinces");
        builder.Property(p => p.Title).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Code).HasMaxLength(10);
        builder.HasIndex(p => p.Title).IsUnique();
        builder.HasData(IranLocationSeed.Provinces);
    }
}
