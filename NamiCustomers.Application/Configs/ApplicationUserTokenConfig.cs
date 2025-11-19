
using NamiCustomers.Domain.Entities.Account;

namespace NamiVIP.Application.Configs;

public class ApplicationUserTokenConfig : IEntityTypeConfiguration<ApplicationUserToken>
{
    public void Configure(EntityTypeBuilder<ApplicationUserToken> builder)
    {
        // تنظیم جدول سفارشی برای UserTokens

        builder.ToTable("AspNetUserTokens"); // استفاده از همان نام جدول

       
        
        // تنظیمات فیلدهای جدید
        builder.Property(t => t.TokenType)
            .IsRequired()
            .HasDefaultValue(TokenType.AccessToken);

        builder.Property(t => t.RefreshToken)
            .HasMaxLength(500);

        builder.Property(t => t.RefreshTokenExp);

        builder.Property(t => t.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(t => t.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        // رابطه با کاربر
        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
     


}