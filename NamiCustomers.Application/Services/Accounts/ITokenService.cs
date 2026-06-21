
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Application.Services.Facades;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Domain.Entities.Subscribers;
using NamiCustomers.Infrastucture.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NamiCustomers.Application.Services.Accounts;

public interface ITokenService
{
    Task<TokenResponse> GenerateAndStoreTokensAsync(ApplicationUser user);
    Task<TokenResponse> GenerateAndStoreTokensAsync(Subscriber user);
    Task<TokenInfo> GetTokenInfoAsync(ApplicationUser user);
    Task<bool> LogoutAsync(string userId);
    Task<ResultDto<TokenResponse>> RefreshTokenAsync(string oldRefreshToken, ApplicationUser user);
    Task<bool> RevokeTokensAsync(ApplicationUser user);
    Task<ResultDto<string>> StoreTokensAsync(ApplicationUser user, string accessToken, string refreshToken);
    Task<bool> ValidateAccessTokenAsync(ApplicationUser user, string accessToken);
}

public class TokenService(
    ILogger<TokenService> logger,
    IConfiguration configuration,
    RoleManager<ApplicationRole> roleManager,
    UserManager<ApplicationUser> userManager,
    IAppDbContext dbContext,
    ISettingsFacadeService settingsFacadeService) : ITokenService
{
    private async Task<Tuple<string, ClaimsIdentity>> GenerateJwtTokenAsync(ApplicationUser user)
    {
        var userRoles = await userManager.GetRolesAsync(user);
        var key = Encoding.ASCII.GetBytes(settingsFacadeService.JWTSetting.securityKey);
        var secretKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var subscriber = await dbContext.Subscribers.Include(c => c.VehicleModels).FirstOrDefaultAsync(c => c.NationalCode == user.NationalCode);

        var claims = new List<Claim>{
                                        new Claim(ClaimTypes.Name, user?.UserName ?? ""),
                                        new Claim(ClaimTypes.Email, user?.Email ?? ""),
                                        new Claim("NationalCode", user?.NationalCode ?? ""),
                                        new Claim("Mobile", subscriber?.Mobile ?? ""),
                                        new Claim("UserId",user?.Id ?? ""),
                                        new Claim("FullName",$"{subscriber?.Name ?? ""} {subscriber?.Family ?? ""}"),
                                        new Claim(ClaimTypes.NameIdentifier, user?.Id ?? ""),
                                        //new Claim("Subscriber", JsonConvert.SerializeObject(subscriber))//alidiablo
                                    };
        var rolesDtos = await roleManager.Roles.Where(c => userRoles.Contains(c.Name)).Select(c => new RoleDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToListAsync();

        foreach (var role in rolesDtos)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
            claims.Add(new Claim("PersianRole", role.Description));
        }

        var token = new JwtSecurityToken(
            issuer: settingsFacadeService.JWTSetting.validIssuer,
            audience: settingsFacadeService.JWTSetting.validAudience,
            claims: claims,
             notBefore: DateTime.UtcNow,
             expires: DateTime.UtcNow.AddMinutes(double.Parse(settingsFacadeService.JWTSetting.expiryInMinutes.ToString())),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.WriteToken(token);
        //var securityToken = tokenHandler.ReadJwtToken(jwtToken);//diablo why signingCredentials is null???? 

        var refreshToken = GenerateRefreshToken();
        // ذخیره توکن در جدول AspNetUserTokens
        await StoreTokensAsync(user,
            jwtToken,
            refreshToken
            );



        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new Tuple<string, ClaimsIdentity>(jwtToken, claimsIdentity);
    }

    private async Task<Tuple<string, ClaimsIdentity>> GenerateJwtTokenAsync(Subscriber user)
    {
        //var userRoles = await userManager.GetRolesAsync(user);
        var key = Encoding.ASCII.GetBytes(settingsFacadeService.JWTSetting.securityKey);
        var secretKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var subscriber = await dbContext.Subscribers.Include(c => c.VehicleModels).FirstOrDefaultAsync(c => c.NationalCode == user.NationalCode);

        var claims = new List<Claim>{
                                        new Claim(ClaimTypes.Name, subscriber?.Name ?? ""),
                                        new Claim("NationalCode", subscriber?.NationalCode ?? ""),
                                        new Claim("Mobile", subscriber?.Mobile ?? ""),
                                        new Claim("UserId",subscriber?.Id.ToString() ?? ""),
                                        new Claim("FullName",$"{subscriber?.Name ?? ""} {subscriber?.Family ?? ""}"),
                                        new Claim(ClaimTypes.NameIdentifier, subscriber?.Id.ToString() ?? ""),
                                        //new Claim(ClaimTypes.Role, BuiltInRole.User.ToString())
                                    };

        var token = new JwtSecurityToken(
            issuer: settingsFacadeService.JWTSetting.validIssuer,
            audience: settingsFacadeService.JWTSetting.validAudience,
            claims: claims,
             notBefore: DateTime.UtcNow,
             expires: DateTime.UtcNow.AddMinutes(double.Parse(settingsFacadeService.JWTSetting.expiryInMinutes.ToString())),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.WriteToken(token);
        //var securityToken = tokenHandler.ReadJwtToken(jwtToken);//diablo why signingCredentials is null???? 

        var refreshToken = GenerateRefreshToken();
        // ذخیره توکن در جدول AspNetUserTokens
        //await StoreTokensAsync(user,
        //    jwtToken,
        //    refreshToken
        //    );



        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new Tuple<string, ClaimsIdentity>(jwtToken, claimsIdentity);
    }

    /// <summary>
    /// ذخیره توکن دسترسی و رفرش توکن با UserManager
    /// </summary>
    /// <param name="user"></param>
    /// <param name="accessToken"></param>
    /// <param name="refreshToken"></param>
    /// <param name="refreshTokenExpiryMinutes"></param>
    /// <returns></returns>
    public async Task<ResultDto<string>> StoreTokensAsync(ApplicationUser user, string accessToken, string refreshToken)
    {
        try
        {

            int refreshTokenExpiryMinutes = settingsFacadeService.JWTSetting.expiryInMinutes;
            var refreshTokenExp = DateTime.UtcNow.AddMinutes(refreshTokenExpiryMinutes);

            // ذخیره Access Token با UserManager
            var result = await userManager.SetAuthenticationTokenAsync(
                user,
                settingsFacadeService.JWTSetting.LogInProvider,
              TokenType.AccessToken.GetEnumDescription(),
                accessToken);

            if (!result.Succeeded)
            {
                return ResultDto.Failure<string>("خطا در ذخیره Access Token");
            }

            // ذخیره Refresh Token با UserManager
            result = await userManager.SetAuthenticationTokenAsync(
                user,
               settingsFacadeService.JWTSetting.LogInProvider,
              TokenType.RefreshToken.GetEnumDescription(),
                refreshToken);

            if (!result.Succeeded)
            {
                return ResultDto.Failure<string>("خطا در ذخیره Refresh Token");
            }

            // ذخیره تاریخ انقضای Refresh Token
            result = await userManager.SetAuthenticationTokenAsync(
                user,
                settingsFacadeService.JWTSetting.LogInProvider,
                TokenType.RefreshTokenExp.GetEnumDescription(),
                refreshTokenExp.ToString("O")); // فرمت ISO 8601

            if (!result.Succeeded)
            {
                return ResultDto.Failure<string>("خطا در ذخیره تاریخ انقضای Refresh Token");
            }

            // ذخیره نوع توکن
            result = await userManager.SetAuthenticationTokenAsync(
                user,
                settingsFacadeService.JWTSetting.LogInProvider,
                "TokenType",
                TokenType.Both.ToString());


            return ResultDto.Success<string>("توکن‌ها با موفقیت ذخیره شدند.");
        }

        catch (Exception ex)
        {
            return ResultDto.Failure<string>($"خطا در ذخیره توکن‌ها: {ex.Message}");
        }
    }

    // تولید و ذخیره توکن جدید
    public async Task<TokenResponse> GenerateAndStoreTokensAsync(ApplicationUser user)
    {
        var accessToken = await GenerateJwtTokenAsync(user);
        var refreshToken = GenerateRefreshToken();

        var storeResult = await StoreTokensAsync(user, accessToken.Item1, refreshToken);

        if (storeResult.Succeeded)
        {

            int expiryMinutes = settingsFacadeService.JWTSetting.expiryInMinutes;

            return new TokenResponse
            {
                AccessToken = accessToken.Item1,
                ClaimsIdentity = accessToken.Item2,
                RefreshToken = refreshToken,
                Expiration = DateTime.UtcNow.AddMinutes(expiryMinutes),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(1),
                TokenType = "Bearer"
            };
        }

        throw new Exception($"Failed to store tokens: {storeResult.Message}");
    }

    public async Task<TokenResponse> GenerateAndStoreTokensAsync(Subscriber user)
    {
        var accessToken = await GenerateJwtTokenAsync(user);
        var refreshToken = GenerateRefreshToken();

        //var storeResult = await StoreTokensAsync(user, accessToken.Item1, refreshToken);

        int expiryMinutes = settingsFacadeService.JWTSetting.expiryInMinutes;

        return new TokenResponse
        {
            AccessToken = accessToken.Item1,
            ClaimsIdentity = accessToken.Item2,
            RefreshToken = refreshToken,
            Expiration = DateTime.UtcNow.AddMinutes(expiryMinutes),
            RefreshTokenExpiration = DateTime.UtcNow.AddDays(1),
            TokenType = "Bearer"
        };

        //throw new Exception($"Failed to store tokens: {storeResult.Message}");
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        try
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.AccessToken.GetEnumDescription());
                await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshToken.GetEnumDescription());
                await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshTokenExp.GetEnumDescription());
                await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, "TokenType");
            }


            logger.LogInformation("تمامی توکن‌های کاربر {UserId} غیرفعال شدند.", userId);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "خطا در خروج کاربر {UserId}", userId);
            return false;
        }
    }


    // رفرش توکن
    public async Task<ResultDto<TokenResponse>> RefreshTokenAsync(string oldRefreshToken, ApplicationUser user)
    {
        try
        {
            DateTime? refreshTokenExp = null;
            string? storedRefreshToken = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, "RefreshToken");
            var refreshTokenExpStr = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshTokenExp.GetEnumDescription());

            if (!string.IsNullOrEmpty(refreshTokenExpStr))
            {
                refreshTokenExp = DateTime.Parse(refreshTokenExpStr);
            }

            if (storedRefreshToken == oldRefreshToken && refreshTokenExp > DateTime.UtcNow)
            {
                user = user;//todo moradi
            }

            if (user == null)
            {
                return ResultDto.Failure<TokenResponse>("Refresh Token معتبر نیست");
            }

            // تولید توکن جدید
            var newAccessToken = await GenerateJwtTokenAsync(user);
            var newRefreshToken = GenerateRefreshToken();

            // ذخیره توکن‌های جدید
            var storeResult = await StoreTokensAsync(user, newAccessToken.Item1, newRefreshToken);

            if (storeResult.Succeeded)
            {
                var result = new TokenResponse
                {
                    AccessToken = newAccessToken.Item1,
                    RefreshToken = newRefreshToken,
                    Expiration = DateTime.UtcNow.AddMinutes(settingsFacadeService.JWTSetting.expiryInMinutes),
                    RefreshTokenExpiration = DateTime.UtcNow.AddDays
                    (1),
                    TokenType = "Bearer"
                };

                return ResultDto.Success<TokenResponse>(result);
            }

            return ResultDto.Failure<TokenResponse>("خطا در تولید رفرش جدید");
        }
        catch (Exception ex)
        {
            return ResultDto.Failure<TokenResponse>($"خطا در رفرش توکن: {ex.Message}");
        }
    }

    // بررسی اعتبار توکن
    public async Task<bool> ValidateAccessTokenAsync(ApplicationUser user, string accessToken)
    {
        try
        {
            var storedToken = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.AccessToken.GetEnumDescription());
            return storedToken == accessToken;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // دریافت اطلاعات توکن
    public async Task<TokenInfo> GetTokenInfoAsync(ApplicationUser user)
    {
        var accessToken = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.AccessToken.GetEnumDescription());
        var refreshToken = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshToken.GetEnumDescription());
        var refreshTokenExpStr = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshTokenExp.GetEnumDescription());
        var tokenTypeStr = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, "TokenType");

        DateTime? refreshTokenExp = null;
        if (!string.IsNullOrEmpty(refreshTokenExpStr))
        {
            refreshTokenExp = DateTime.Parse(refreshTokenExpStr);
        }

        int tokenType = 0;
        if (!string.IsNullOrEmpty(tokenTypeStr))
        {
            int.TryParse(tokenTypeStr, out tokenType);
        }

        return new TokenInfo
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            RefreshTokenExpiration = refreshTokenExp,
            TokenType = tokenType,
            HasValidToken = !string.IsNullOrEmpty(accessToken) &&
                          !string.IsNullOrEmpty(refreshToken) &&
                          refreshTokenExp > DateTime.UtcNow
        };
    }

    // غیرفعال کردن توکن‌های کاربر
    public async Task<bool> RevokeTokensAsync(ApplicationUser user)
    {
        try
        {
            // حذف تمام توکن‌های مربوط به برنامه
            await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.AccessToken.GetEnumDescription());
            await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshToken.GetEnumDescription());
            await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.RefreshTokenExp.GetEnumDescription());
            await userManager.RemoveAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, "TokenType");

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // تولید رفرش توکن
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}


public class TokenResponse
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? Expiration { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public string? TokenType { get; set; }
    public string? Error { get; set; }

    public ClaimsIdentity ClaimsIdentity { get; set; }
}

public class TokenInfo
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
    public int TokenType { get; set; }
    public bool HasValidToken { get; set; }
}

public enum BuiltInRole
{
    Guest =0,
    User = 1,
    Admin
}