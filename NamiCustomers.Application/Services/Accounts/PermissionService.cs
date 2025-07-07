using Microsoft.AspNetCore.Identity;
using NamiCustomers.Domain.Entities.Account;
using System.Security.Claims;

namespace NamiCustomers.Application.Services.Accounts;
public class PermissionService : IPermissionService
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public PermissionService(
        RoleManager<ApplicationRole> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // Add permission to role (AspNetRoleClaims)
    public async Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

        var existingClaims = await _roleManager.GetClaimsAsync(role);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            return IdentityResult.Success;

        return await _roleManager.AddClaimAsync(role, new Claim("Permission", permission));
    }

    // Remove permission from role
    public async Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null)
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

        var claims = await _roleManager.GetClaimsAsync(role);
        var claim = claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);

        if (claim != null)
            return await _roleManager.RemoveClaimAsync(role, claim);

        return IdentityResult.Success;
    }

    // Add permission directly to user (AspNetUserClaims)
    public async Task<IdentityResult> AddPermissionToUserAsync(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var existingClaims = await _userManager.GetClaimsAsync(user);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            return IdentityResult.Success;

        return await _userManager.AddClaimAsync(user, new Claim("Permission", permission));
    }

    // Remove permission from user
    public async Task<IdentityResult> RemovePermissionFromUserAsync(string userId, string permission)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var claims = await _userManager.GetClaimsAsync(user);
        var claim = claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);

        if (claim != null)
            return await _userManager.RemoveClaimAsync(user, claim);

        return IdentityResult.Success;
    }

    // Get all permissions for a user (combines role and user-specific permissions)
    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return new List<string>();

        // Get user's roles
        var roles = await _userManager.GetRolesAsync(user);

        // Get role claims
        var roleClaims = new List<Claim>();
        foreach (var roleName in roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                roleClaims.AddRange(await _roleManager.GetClaimsAsync(role));
            }
        }

        // Get user claims
        var userClaims = await _userManager.GetClaimsAsync(user);

        // Combine and filter permissions
        var allClaims = roleClaims.Concat(userClaims);
        return allClaims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .Distinct()
            .ToList();
    }

    // Check if user has specific permission
    public async Task<bool> UserHasPermissionAsync(string userId, string permission)
    {
        var permissions = await GetUserPermissionsAsync(userId);
        return permissions.Contains(permission);
    }
}