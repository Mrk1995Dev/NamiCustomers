using Microsoft.Extensions.Logging;
using NamiCustomers.Abstractions.Dtos.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.Application.Services.Menus
{
    public interface IMenuService
    {
        Task<List<MenuDto>> GetMenuStructureAsync();
        Task<List<MenuDto>> GetUserMenuAsync(ClaimsPrincipal user);
        Task<MenuDto> CreateMenuAsync(CreateMenuDto dto);
        Task UpdateMenuAsync(Guid id, CreateMenuDto dto);
        Task DeleteMenuAsync(Guid id);
    }

    public class MenuService(
        IAppDbContext context,
        ILogger<MenuService> logger) : IMenuService
    {
        public async Task<List<MenuDto>> GetMenuStructureAsync()
        {
            var menus = await context.Menus
                .Include(m => m.SubMenus)
                .Where(m => m.ParentMenuId == null)
                .OrderBy(m => m.Order)
                .ToListAsync();

            return menus.Select(MapToDto).ToList();
        }

        public async Task<List<MenuDto>> GetUserMenuAsync(ClaimsPrincipal user)
        {
            var userRoles = user.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (!userRoles.Any())
                return [];

            var menus = await context.Menus
                .Include(m => m.Roles)
                .Include(m => m.SubMenus)
                    .ThenInclude(sm => sm.Roles)
                .Where(m => m.ParentMenuId == null && m.IsActive)
                .OrderBy(m => m.Order)
                .ToListAsync();

            return menus
                .Where(m => m.Roles.Any(r => userRoles.Contains(r.Name!)))
                .Select(m => FilterMenuByRoles(m, userRoles))
                .Where(m => m != null)
                .Select(m => m!)
                .ToList();
        }

        private MenuDto? FilterMenuByRoles(Menu menu, List<string> userRoles)
        {
            var filteredSubMenus = menu.SubMenus
                .Where(sm => sm.IsActive && sm.Roles.Any(r => userRoles.Contains(r.Name!)))
                .Select(sm => FilterMenuByRoles(sm, userRoles))
                .Where(sm => sm != null)
                .Select(sm => sm!)
                .ToList();

            if (!filteredSubMenus.Any() && !menu.Roles.Any(r => userRoles.Contains(r.Name!)))
                return null;

            var dto = MapToDto(menu);
            dto.SubMenus = filteredSubMenus;
            return dto;
        }

        public async Task<MenuDto> CreateMenuAsync(CreateMenuDto dto)
        {
            var menu = new Menu
            {
                Title = dto.Title,
                Description = dto.Description,
                Icon = dto.Icon,
                Route = dto.Route,
                Order = dto.Order,
                ParentMenuId = dto.ParentMenuId
            };

            if (dto.AllowedRoles.Any())
            {
                var roles = await context.Roles
                    .Where(r => dto.AllowedRoles.Contains(r.Name!))
                    .ToListAsync();
                menu.Roles = roles;
            }

            context.Menus.Add(menu);
            await context.SaveChangesAsync();

            return MapToDto(menu);
        }

        public async Task UpdateMenuAsync(Guid id, CreateMenuDto dto)
        {
            var menu = await context.Menus
                .Include(m => m.Roles)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (menu == null)
                throw new KeyNotFoundException("Menu not found");

            menu.Title = dto.Title;
            menu.Description = dto.Description;
            menu.Icon = dto.Icon;
            menu.Route = dto.Route;
            menu.Order = dto.Order;
            menu.ParentMenuId = dto.ParentMenuId;

            if (dto.AllowedRoles.Any())
            {
                var roles = await context.Roles
                    .Where(r => dto.AllowedRoles.Contains(r.Name!))
                    .ToListAsync();
                menu.Roles = roles;
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteMenuAsync(Guid id)
        {
            var menu = await context.Menus.FindAsync(id);
            if (menu == null)
                throw new KeyNotFoundException("Menu not found");

            // Check if this menu has submenus
            var hasSubMenus = await context.Menus.AnyAsync(m => m.ParentMenuId == id);
            if (hasSubMenus)
                throw new InvalidOperationException("Cannot delete menu with submenus");

            context.Menus.Remove(menu);
            await context.SaveChangesAsync();
        }

        private static MenuDto MapToDto(Menu menu) => new()
        {
            Id = menu.Id,
            Title = menu.Title,
            Description = menu.Description,
            Icon = menu.Icon,
            Route = menu.Route,
            Order = menu.Order,
            IsActive = menu.IsActive,
            ParentMenuId = menu.ParentMenuId,
            SubMenus = menu.SubMenus
                .OrderBy(sm => sm.Order)
                .Select(MapToDto)
                .ToList()
        };
    }
}
