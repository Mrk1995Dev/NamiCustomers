using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.Utilities;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System.Security.Claims;

namespace NamiCustomers.Application.Services.Accounts;

public class AccountService(
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager, IMapper mapper,
    IAppDbContext context,
    ITokenService tokenService) : IAccountService
{
    public async Task<ResultDto<LoginResponseDto>> CheckSubscriberRegisteredAsync(string phoneNumber, string nationalCode)
    {
        //var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == phoneNumber).SingleOrDefaultAsync();
        var subscriber = await context.Subscribers.SingleOrDefaultAsync(s => s.Mobile == phoneNumber);
        if (subscriber != null)
        {
            var tokenResponse = await tokenService.GenerateAndStoreTokensAsync(subscriber);
            var result = ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = tokenResponse.RefreshToken, Token = tokenResponse.AccessToken, NationalCode = subscriber.NationalCode, Mobile = subscriber.Mobile, Id = subscriber.Id.ToString(), FirstName = subscriber.Name, LastName = subscriber.Family });
            return result;
        }

        else
        {
            var registerUserResult = await context.Subscribers.AddAsync(new Domain.Entities.Subscribers.Subscriber
            {
                Name = string.Empty,
                Family = string.Empty,
                Mobile = phoneNumber,
                NationalCode = nationalCode,
            });

            if ((await context.SaveChangesAsync()) > 0)
            {
                var newUser = await userManager.Users.WhereIf(true, c => c.Mobile == phoneNumber).SingleOrDefaultAsync();
                var tokenResponse = await tokenService.GenerateAndStoreTokensAsync(newUser);
                var result = ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = tokenResponse.RefreshToken, Token = tokenResponse.AccessToken, NationalCode = subscriber.NationalCode, Mobile = subscriber.Mobile, Id = subscriber.Id.ToString(), FirstName = subscriber.Name, LastName = subscriber.Family });
                return result;
            }


            else
                return new ResultDto<LoginResponseDto>("خطا در ایجاد توکن مربوطه", false);
        }
    }

    // Add permission to role (AspNetRoleClaims)
    public async Task<IdentityResult> AddPermissionToRoleAsync(string roleName, string permission)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

        var existingClaims = await roleManager.GetClaimsAsync(role);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            return IdentityResult.Success;

        return await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
    }

    // Remove permission from role
    public async Task<IdentityResult> RemovePermissionFromRoleAsync(string roleName, string permission)
    {
        var role = await roleManager.FindByNameAsync(roleName);
        if (role == null)
            return IdentityResult.Failed(new IdentityError { Description = "Role not found" });

        var claims = await roleManager.GetClaimsAsync(role);
        var claim = claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);

        if (claim != null)
            return await roleManager.RemoveClaimAsync(role, claim);

        return IdentityResult.Success;
    }

    // Add permission directly to user (AspNetUserClaims)
    public async Task<IdentityResult> AddPermissionToUserAsync(string userId, string permission)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var existingClaims = await userManager.GetClaimsAsync(user);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            return IdentityResult.Success;

        return await userManager.AddClaimAsync(user, new Claim("Permission", permission));
    }

    // Remove permission from user
    public async Task<IdentityResult> RemovePermissionFromUserAsync(string userId, string permission)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return IdentityResult.Failed(new IdentityError { Description = "User not found" });

        var claims = await userManager.GetClaimsAsync(user);
        var claim = claims.FirstOrDefault(c => c.Type == "Permission" && c.Value == permission);

        if (claim != null)
            return await userManager.RemoveClaimAsync(user, claim);

        return IdentityResult.Success;
    }

    // Get all permissions for a user (combines role and user-specific permissions)
    public async Task<List<string>> GetUserPermissionsAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return new List<string>();

        // Get user's roles
        var roles = await userManager.GetRolesAsync(user);

        // Get role claims
        var roleClaims = new List<Claim>();
        foreach (var roleName in roles)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                roleClaims.AddRange(await roleManager.GetClaimsAsync(role));
            }
        }

        // Get user claims
        var userClaims = await userManager.GetClaimsAsync(user);

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

    public async Task<ResultDto<UserDto>> GetByNationalCodeAsync(string nationalCode)
    {
        try
        {
            var user = await userManager.Users.FirstOrDefaultAsync(cu => cu.NationalCode == nationalCode);
            var userDto = mapper.Map<UserDto>(user);
            return ResultDto.Success<UserDto>(userDto);
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<UserDto>(ex.Message);
        }
    }

    public async Task<ResultDto> ResetPasswordAsync(ResetPasswordDto reset)
    {
        var subscriber = await context.Subscribers.FirstOrDefaultAsync(s => s.NationalCode == reset.UserId);

        if (subscriber is null)
            return ResultDto.Failure("کاربر با مشخصات مربوطه یافت نشد.");

        if (reset.Password != reset.ConfirmPassword)
            return ResultDto.Failure("کاربر گرامی, رمزعبور با تکرار رمز عبور باید یکسان باشد.");

        if (string.IsNullOrWhiteSpace(reset.Password))
            return ResultDto.Failure("کاربر گرامی, لطفا رمز عبور را وارد کنید.");

        string hashPassword = BCrypt.Net.BCrypt.HashPassword(reset.Password);

        subscriber.HashPassword = hashPassword;

        context.Subscribers.Update(subscriber);

        if(( await context.SaveChangesAsync()) > 0)
            return ResultDto.Success("رمز عبور با موفقیت تغییر یافت.");
        else
            return ResultDto.Failure("خطا در تغییر رمز عبور, لطفا مجددا تلاش کنید.");
    }
}