using Microsoft.AspNetCore.Identity;

namespace NamiCustomers.Application.Services.Accounts
{
    public interface IPermissionService
    {
        Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission);
        Task<IdentityResult> AddPermissionToUserAsync(string userId, string permission);
        Task<List<string>> GetUserPermissionsAsync(string userId);
        Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission);
        Task<IdentityResult> RemovePermissionFromUserAsync(string userId, string permission);
        Task<bool> UserHasPermissionAsync(string userId, string permission);
    }
}