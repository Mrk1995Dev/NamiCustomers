using NamiCustomers.Application.Seeds;

namespace NamiCustomers.Application.Configs;

public class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");
        builder.Property(c => c.Title).IsRequired().HasMaxLength(100);
        builder.HasOne(c => c.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => new { c.ProvinceId, c.Title }).IsUnique();
        builder.HasData(IranLocationSeed.Cities);
    }
}
