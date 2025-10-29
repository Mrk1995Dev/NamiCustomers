using AutoMapper;
using AutoMapper.Configuration.Annotations;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Application.Services.Accounts;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Infrastucture.ExternalServices.Email;
using NamiCustomers.Infrastucture.ExternalServices.SmsServices;
using NamiCustomers.Infrastucture.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[Authorize]
public class AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager,IMapper mapper, RoleManager<ApplicationRole> roleManager,
        SignInManager<ApplicationUser> signInManager, IAccountService accountService, ISubscriberService subscriberService, IMailService mailService, IUrlHelperFactory urlHelperFactory
  , ISmsService smsService) : ControllerBase
{
    
    [HttpGet("[action]")]
    public async Task<IActionResult> GetByNationalCodeAsync(string nationalCode)
    {
        var result = await accountService.GetByNationalCodeAsync(nationalCode);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto)
    {
        await signInManager.SignOutAsync();
        var user = new ApplicationUser()
        {
            Email = myAccountinfoDto.Email,
            EmailConfirmed = myAccountinfoDto.EmailConfirmed,
            Id = myAccountinfoDto.Id,
            PhoneNumber = myAccountinfoDto.PhoneNumber,
            PhoneNumberConfirmed = myAccountinfoDto.PhoneNumberConfirmed,
            TwoFactorEnabled = myAccountinfoDto.TwoFactorEnabled,
            UserName = myAccountinfoDto.UserName,
        };

        var userIdentity = User.Identity;
        var result = await signInManager.PasswordSignInAsync(user, myAccountinfoDto.Password, myAccountinfoDto.IsPersistent, true);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RegisterAsync(RegisterUserDto registerUserDto)
    {

        var newUser = new ApplicationUser()
        {
            FirstName = registerUserDto.FirstName,
            LastName = registerUserDto.LastName,
            Email = registerUserDto.Email,
            UserName = registerUserDto.Email,
            PassWord = registerUserDto.Password,
        };

        var createResult = await userManager.CreateAsync(newUser, registerUserDto.Password);
        if (createResult.Succeeded)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string callbackUrl = Url.Action("ConfirmEmail", "Account", new
            {
                UserId = newUser.Id
            ,
                token
            }
            , protocol: Request.Scheme);


            registerUserDto.CallbakUrl = registerUserDto.CallbakUrl.Replace("TEMPTOKEN", token);

            string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href='{registerUserDto.CallbakUrl}'> Link </a>";
            await mailService.SendEmailAsync(new MailRequest { Body = body, Subject = "فعال سازی حساب کاربری", ToEmail = newUser.Email });
            var result = ResultDto.Success<UserDto>(mapper.Map<UserDto>(newUser));

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        return BadRequest(ResultDto.Failure<UserDto>(
          string.Join(",", createResult.Errors.Select(c => c.Description))
          ));
    }


    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> GetOtpAsync([FromQuery] string mobile, [FromQuery] string nationalCode)
    {
        var result = await subscriberService.GetOtpAsync(mobile, nationalCode);
        if (result.Succeeded)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> LogInByOtp([FromQuery] string otpCode)
    {
        if (!string.IsNullOrEmpty(otpCode))
        {
            var otpResult = await subscriberService.SendOtpAsync(otpCode);
            if (otpResult.Succeeded)
            {
                var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == otpResult.Data.Mobile).SingleOrDefaultAsync();
                if (user != null)
                {
                    var token = $"{GenerateJwtToken(user).Result}";
                    var refreshToken = $"{GenerateJwtToken(user).Result}";
                    var result=   ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, NationalCode = user.NationalCode, Mobile = user.PhoneNumber, Id = user.Id, FirstName = user.FirstName, LastName = user.LastName });
                    return Ok(result);
                }
                else
                {
                    var registerUserResult = await RegisterUser(new RegisterModelDto
                    {
                        Email = $"{otpResult.Data.Mobile}@namikhodro.com",
                        FirstName = $"{otpResult.Data.Mobile}",
                        
                        LastName = $"{otpResult.Data.Mobile}",
                        Mobile = otpResult.Data.Mobile,
                        Password = $"Nn@{otpResult.Data.Mobile}",
                        NationalCode = otpResult.Data.NationalCode
                    });

                    if (registerUserResult)
                    {
                        var newUser = await userManager.Users.WhereIf(true, c => c.PhoneNumber == otpResult.Data.Mobile).SingleOrDefaultAsync();
                        var token = $"{GenerateJwtToken(newUser)}";
                        var refreshToken = $"{GenerateJwtToken(newUser)}";
                        var result = ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = newUser.Email,FirstName=newUser.FirstName,LastName=newUser.LastName,Mobile=newUser.PhoneNumber,NationalCode=newUser.NationalCode,Id=newUser.Id });
                        return Ok(result);
                    }
                    return BadRequest(ResultDto.Failure<LoginResponseDto>(Infrastucture.Properties.Resources.errNotFound));
                }
            }
            return BadRequest(ResultDto.Failure<LoginResponseDto>(Infrastucture.Properties.Resources.errOtpInvalid));
        }
        return BadRequest(ResultDto.Failure<LoginResponseDto>(Infrastucture.Properties.Resources.errOtpIsNotNull));
    }

    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto reset)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        if (reset.Password != reset.ConfirmPassword)
        {
            return BadRequest();
        }
        var user =await userManager.FindByEmailAsync(reset.UserId);
        if (user == null)
        {
            return BadRequest();
        }

        var result =await userManager.ResetPasswordAsync(user, reset.Token.Replace(" ", "+"), reset.Password);

        if (result.Succeeded)
        {
            var currentUser =await  userManager.FindByEmailAsync(reset.UserId);
            if (currentUser == null)
            {
                return BadRequest(ResultDto.Failure<IdentityResult>(Infrastucture.Properties.Resources.errNotFound));
            }
            currentUser.PassWord = reset.Password;
            var updateResult = await userManager.UpdateAsync(currentUser);
            if (!updateResult.Succeeded)
            {
                return BadRequest(ResultDto.Failure<IdentityResult>(Infrastucture.Properties.Resources.errEdited));
            }
            return Ok(ResultDto.Success<IdentityResult>(updateResult));
        }
        else
        {
            return BadRequest(ResultDto.Failure<IdentityResult>(string.Join(",", result.Errors.Select(c => c.Description).ToList())));
        }

    }



    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        if (!ModelState.IsValid)
        {
            string errorResponse = string.Join(",", ModelState.Values.Select(c => c.Errors.Select(r => r.ErrorMessage).FirstOrDefault()).ToList());
            return BadRequest(ResultDto.Failure<ForgotPasswordResponse>(errorResponse));
        }

        var user = await userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
        if (user == null || userManager.IsEmailConfirmedAsync(user).Result == false)
        {
            return BadRequest(ResultDto.Failure<ForgotPasswordResponse>("ممکن است ایمیل وارد شده معتبر نباشد! و یا اینکه ایمیل خود را تایید نکرده باشید"));
        }

        string token = userManager.GeneratePasswordResetTokenAsync(user).Result;

        forgotPasswordRequestDto.CallBAckUrl = forgotPasswordRequestDto.CallBAckUrl.Replace("TEMPTOKEN", token);


        string body = $@"برای تنظیم مجدد کلمه عبور بر روی لینک زیر کلیک کنید <br/> <a href='{forgotPasswordRequestDto.CallBAckUrl}'> link reset Password </a>";
        await mailService.SendEmailAsync(new MailRequest { ToEmail = user.Email, Body = body, Subject = "فراموشی رمز عبور" });

        return Ok(ResultDto.Success<ForgotPasswordResponse>(new ForgotPasswordResponse
        {
            Email = forgotPasswordRequestDto.Email,
            Token = token,
            Message = "لینک تنظیم مجدد کلمه عبور برای ایمیل شما ارسال شد"
        }));
    }





    [HttpPost("[action]")]
    [AllowAnonymous]
    public async Task<IActionResult> LogIn(LoginModelDto model)
    {
        var user =await  userManager.FindByNameAsync(model.Email);
        await signInManager.SignOutAsync();
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPersistent, false);
        if (result.Succeeded)
        {
            var token = $"{GenerateJwtToken(user)}";
            var refreshToken = $"{GenerateJwtToken(user)}";
            return Ok(ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName }));
        }
        return Unauthorized();
    }



    [HttpGet("[action]")]
    public async Task<IActionResult> GetTokenAsync([FromQuery] LoginModelDto model)
    {
        if (!string.IsNullOrEmpty(model.Mobile))
        {
            var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == model.Mobile).SingleOrDefaultAsync();
            if (user != null)
            {
                var token = $"{GenerateJwtToken(user)}";
                var refreshToken = $"{GenerateJwtToken(user)}";
                return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName }));
            }
            return Unauthorized();
        }

        //a.moradi@namikhodro.com Aa12334566*
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var token = $"{GenerateJwtToken(user)}";
            var refreshToken = $"{GenerateJwtToken(user)}";
            return Ok(ResultDto.Success<LoginResponseDto>(new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName }));
        }

        return Unauthorized();
    }

    [HttpPost("[action]")]
    private async Task<bool> RegisterUser([FromBody] RegisterModelDto model)
    {
        var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), NationalCode = model.NationalCode, UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.Mobile, PhoneNumberConfirmed = true, PassWord = model.Password,
        EmailConfirmed=true};

        try
        {
            var result = userManager.CreateAsync(user, model.Password).Result;

            if (!result.Succeeded)
            {
                return false;
            }

            var subResult = await subscriberService.RegisterAsync(new SubscriberDto
            {
                Name = user.FirstName,
                Family = user.LastName,
                Mobile = user.PhoneNumber,
                NationalCode = user.NationalCode,
                Phone = user.PhoneNumber,

            });

            if (subResult.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, MyRoles.Subscriber);
            }
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> ConfirmEmail(ConfirmRequest confirmRequest)
    {

        var user =await userManager.FindByIdAsync(confirmRequest.UserId);
        if (user == null)
        {
            return BadRequest(ResultDto.Failure<ConfirmResponse>(Infrastucture.Properties.Resources.errNotFound));
        }

        var result =await  userManager.ConfirmEmailAsync(user, confirmRequest.Token);
        if (result.Succeeded)
        {
            return BadRequest(ResultDto.Success<ConfirmResponse>(new ConfirmResponse { IsSuccess = true }));

        }
        return BadRequest(ResultDto.Failure<ConfirmResponse>(string.Join(",", result.Errors.Select(c => c.Description).ToList())));

    }

    [HttpPost("[action]")]
    public async Task<IActionResult> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
    {
        var user = await userManager.FindByNameAsync(User.Identity.Name);
        if (user == null)
        {
            return BadRequest(ResultDto.Failure<IdentityResult>(Infrastucture.Properties.Resources.errNotFound));
        }
        var setResult = await userManager.SetPhoneNumberAsync(user, phoneNumberDto.PhoneNumber);
        if (!setResult.Succeeded)
        {
            return BadRequest(ResultDto.Failure<IdentityResult>(string.Join(",", setResult.Errors.Select(c => c.Description).ToList())));
        }

        string code = await userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumberDto.PhoneNumber);

        var sendResult = await smsService.SendSms(phoneNumberDto.PhoneNumber, code);
        if (sendResult.IsSuccessStatusCode)
        {
            return Ok(ResultDto.Success<IdentityResult>(new IdentityResult()));
        }
        else
        {
            return BadRequest(ResultDto.Failure<IdentityResult>(sendResult.ReasonPhrase));
        }
    }


    //[Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberDto verify)
    {
        var user =await  userManager.FindByNameAsync(User.Identity.Name);
        bool resultVerify =await  userManager.VerifyChangePhoneNumberTokenAsync(user, verify.Code, verify.PhoneNumber);
        if (resultVerify == false)
        {
            return BadRequest(ResultDto.Failure<ConfirmResponse>(string.Join(",", new List<string> { $"کد وارد شده برای شماره {verify.PhoneNumber} اشتباه است" })));
        }
        else
        {
            user.PhoneNumberConfirmed = true;
            var resultUpdate =await  userManager.UpdateAsync(user);
        }
        return Ok(ResultDto.Success<ConfirmResponse>(new ConfirmResponse()));
    }



    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var userRoles =await  userManager.GetRolesAsync(user);
        var subscriber = (await subscriberService.GetByNationalCodeAsync(user.NationalCode)).Data;

        var jwtSettings = configuration.GetSection("JWTSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["securityKey"]);

        var claims = new List<Claim>{
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim(ClaimTypes.Email, user.Email),
                                        new Claim("NationalCode", user.NationalCode),
                                        new Claim("Mobile", user.PhoneNumber),
                                        new  System.Security.Claims.Claim("UserId",user.Id),
                                        new  System.Security.Claims.Claim("FullName",$"{user.FirstName} {user.LastName}"),
                                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                                        new Claim("Subscriber", JsonConvert.SerializeObject(subscriber))
                                    };
        var rolesDtos = await roleManager.Roles.Where(c => userRoles.Contains(c.Name)).Select(c => new RoleDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description
        }).ToListAsync();

        foreach (var role in rolesDtos)
        {
            claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role.Name));
            claims.Add(new System.Security.Claims.Claim("PersianRole", role.Description));
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["expiryInMinutes"])),
            Issuer = jwtSettings["validIssuer"],
            Audience = jwtSettings["validAudience"],

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        var securityToken = (JwtSecurityToken)tokenHandler.ReadJwtToken(tokenString);
        // Save custom data in cookie
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddHours(2)
        };

        Response.Cookies.Append("UserData", "YourCustomDataHere", cookieOptions);
        Response.Cookies.Append("AuthToken", tokenString, cookieOptions);

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));


        return tokenString;
    }

}







