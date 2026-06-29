using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Security.Dto;

namespace NamiCustomers.Application.Services.Accounts
{
    public interface IAccountService
    {
        Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission);
        Task<IdentityResult> AddPermissionToUserAsync(string userId, string permission);
        Task<List<string>> GetUserPermissionsAsync(string userId);
        Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission);
        Task<IdentityResult> RemovePermissionFromUserAsync(string userId, string permission);
        Task<bool> UserHasPermissionAsync(string userId, string permission);
        Task<ResultDto<LoginResponseDto>> CheckSubscriberRegisteredAsync(string phoneNumber, string nationalCode);
        Task<ResultDto<UserDto>> GetByNationalCodeAsync(string nationalCode);
        Task<ResultDto> ResetPasswordAsync(ResetPasswordDto reset);
    }
}