using NamiCustomers.Domain.Entities.Account;

namespace NamiCustomers.Application.Configs;

public class MenuConfig : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {

        builder.HasKey(m => m.Id);

        builder.HasMany(m => m.SubMenus)
                .WithOne(m => m.ParentMenu)
                .HasForeignKey(m => m.ParentMenuId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(m => m.Roles)
                .WithMany(r => r.Menus)
                .UsingEntity<Dictionary<string, object>>(
                    "MenuRoles",
                    j => j.HasOne<ApplicationRole>().WithMany().HasForeignKey("RoleId"),
                    j => j.HasOne<Menu>().WithMany().HasForeignKey("MenuId")
                );
        
    }
}