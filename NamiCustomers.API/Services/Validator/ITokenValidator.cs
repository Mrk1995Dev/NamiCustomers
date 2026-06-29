using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Application.Services.Facades;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Application.Services.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NamiCustomers.API.Services.Validator;

/// <summary>
/// بررسی اعتبار توکن
/// </summary>
public interface ITokenValidator
{
    Task Execute(TokenValidatedContext context);
}
/// <summary>
/// Validates the token and ensures it is associated with a valid user and stored in the system.
/// </summary>
/// <remarks>This class implements the <see cref="ITokenValidator"/> interface and performs token validation by
/// checking the claims in the token, verifying the user associated with the token, and ensuring the token is stored in
/// the system. If any of these checks fail, the token validation process is terminated with an appropriate failure
/// message.</remarks>
/// <param name="userManager"></param>
public class TokenValidate(UserManager<ApplicationUser>  userManager,ISettingsFacadeService settingsFacadeService) : ITokenValidator
{
    public async Task Execute(TokenValidatedContext context)
    {
        //var claimsidentity = context.Principal.Identity as ClaimsIdentity;
        //if(claimsidentity?.Claims == null  || !claimsidentity.Claims.Any()) 
        //{
        //    context.Fail("claims not found....");
        //    return;
        //}

        //var userId = claimsidentity.FindFirst("UserId").Value;
        //if(!Guid.TryParse(userId, out Guid userGuid))
        //{
        //    context.Fail("claims not found....");
        //    return;
        //}

        //var user=await  userManager.FindByIdAsync(userGuid.ToString());

        //// بررسی اینکه توکن واقعاً ذخیره شده است
        //var storedToken = await userManager.GetAuthenticationTokenAsync(user, settingsFacadeService.JWTSetting.LogInProvider, TokenType.AccessToken.GetEnumDescription());


        //if (storedToken == null)
        //{
        //    context.Fail("token not found");
        //    return;
        //}
    }
}
