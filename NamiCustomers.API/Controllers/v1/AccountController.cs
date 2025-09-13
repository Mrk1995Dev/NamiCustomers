using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Subscribers;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Infrastucture.ExternalServices.Email;
using NamiCustomers.Infrastucture.ExternalServices.SmsServices;
using NamiCustomers.Infrastucture.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, ISubscriberService subscriberService, IMailService mailService, IUrlHelperFactory urlHelperFactory
  , ISmsService smsService) : ControllerBase
{
    //[HttpGet("[action]")]
    //public async Task<MyAccountinfoDto> FindByNameAsync()
    //{
    //    var user = await userManager.FindByNameAsync(User.Identity.Name);
    //    var myAccount = new MyAccountinfoDto()
    //    {
    //        Email = user.Email,
    //        EmailConfirmed = user.EmailConfirmed,
    //        FullName = $"{user.FirstName} {user.LastName}",
    //        Id = user.Id,
    //        PhoneNumber = user.PhoneNumber,
    //        PhoneNumberConfirmed = user.PhoneNumberConfirmed,
    //        TwoFactorEnabled = user.TwoFactorEnabled,
    //        UserName = user.UserName,
    //    };
    //    return myAccount;
    //}


    //[HttpGet("[action]")]
    //public async Task<IActionResult> GetCurrentUser()
    //{
    //    return Ok(new ResultDto<List<ClaimsIdentity>>("", true, User.Identities.ToList()));
    //}

    [HttpPost("[action]")]
    public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto)
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
        return await signInManager.PasswordSignInAsync(user, myAccountinfoDto.Password, myAccountinfoDto.IsPersistent, true);

    }

    [HttpPost("[action]")]
    public async Task<ResultDto> RegisterAsync(RegisterUserDto registerUserDto)
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
            return new ResultDto(Infrastucture.Properties.Resources.msgSave, true); //new RegisterResponse { IsSuccess = true };
        }

        return new ResultDto(
            Infrastucture.Properties.Resources.errSave
            , false//todo moradi
                   //  new ApiErrorResponse(createResult.Errors.Select(c => new ApiError(c.Code, c.Description)).ToList())
            );
    }


    [HttpGet("[action]")]
    public async Task<ResultDto<SubscriberCodeDto>> GetOtpAsync([FromQuery] string mobile, string nationalCode)
    {
        var result = await subscriberService.GetOtpAsync(mobile, nationalCode);
        return result;
    }

    [HttpGet("[action]")]
    public async Task<ResultDto<LoginResponseDto>> LogInByOtp([FromQuery] string otpCode)
    {
        if (!string.IsNullOrEmpty(otpCode))
        {
            var otpResult = await subscriberService.SendOtpAsync(otpCode);
            if (otpResult.Succeeded)
            {
                var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == otpResult.Data.Mobile).SingleOrDefaultAsync();
                if (user != null)
                {
                    var token = $"{GenerateJwtToken(user)}";
                    var refreshToken = $"{GenerateJwtToken(user)}";
                    return new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, NationalCode = user.NationalCode, Mobile = user.PhoneNumber, Id = user.Id, FirstName = user.FirstName, LastName = user.LastName });
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
                        return new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = newUser.Email });
                    }
                    return new ResultDto<LoginResponseDto>("", false);//todo moradi

                }
            }
            return new ResultDto<LoginResponseDto>(Infrastucture.Properties.Resources.errOtpInvalid, false);
        }
        return new ResultDto<LoginResponseDto>(Infrastucture.Properties.Resources.errOtpIsNotNull, false);
    }

    [HttpPost("[action]")]
    public IActionResult ResetPassword(ResetPasswordDto reset)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        if (reset.Password != reset.ConfirmPassword)
        {
            return BadRequest();
        }
        var user = userManager.FindByEmailAsync(reset.UserId).Result;
        if (user == null)
        {
            return BadRequest();
        }

        var Result = userManager.ResetPasswordAsync(user, reset.Token.Replace(" ", "+"), reset.Password).Result;

        if (Result.Succeeded)
        {
            var currentUser = userManager.FindByEmailAsync(reset.UserId).Result;
            currentUser.PassWord = reset.Password;
            var result = userManager.UpdateAsync(currentUser).Result;

            return Ok(new ResultDto<IdentityResult>(Infrastucture.Properties.Resources.msgSave, false));
        }
        else
        {
            //var errorResponse = new ApiErrorResponse(Result.Errors.Select(e => new ApiError
            //(e.Code,
            // e.Description
            //)).ToList());//todo moradi

            return BadRequest(new ResultDto<IdentityResult>(Infrastucture.Properties.Resources.Error, false));
        }

    }



    [HttpPost("[action]")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestDto forgotPasswordRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResultDto<ForgotPasswordResponse>("ممکن است ایمیل وارد شده معتبر نباشد!", false));
        }

        var user = await userManager.FindByEmailAsync(forgotPasswordRequestDto.Email);
        if (user == null || userManager.IsEmailConfirmedAsync(user).Result == false)
        {
            return BadRequest(new ResultDto<ForgotPasswordResponse>("ممکن است ایمیل وارد شده معتبر نباشد! و یا اینکه ایمیل خود را تایید نکرده باشید", false));
        }

        string token = userManager.GeneratePasswordResetTokenAsync(user).Result;

        forgotPasswordRequestDto.CallBAckUrl = forgotPasswordRequestDto.CallBAckUrl.Replace("TEMPTOKEN", token);


        string body = $@"برای تنظیم مجدد کلمه عبور بر روی لینک زیر کلیک کنید <br/> <a href='{forgotPasswordRequestDto.CallBAckUrl}'> link reset Password </a>";
        await mailService.SendEmailAsync(new MailRequest { ToEmail = user.Email, Body = body, Subject = "فراموشی رمز عبور" });

        return Ok(new ResultDto<ForgotPasswordResponse>("لینک تنظیم مجدد کلمه عبور برای ایمیل شما ارسال شد", true, new ForgotPasswordResponse { Email = forgotPasswordRequestDto.Email, Token = token }));
    }





    [HttpPost("[action]")]
    public async Task<IActionResult> LogIn(LoginModelDto model)
    {
        var user = userManager.FindByNameAsync(model.Email).Result;
        await signInManager.SignOutAsync();
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPersistent, false);
        if (result.Succeeded)
        {
            var token = $"{GenerateJwtToken(user)}";
            var refreshToken = $"{GenerateJwtToken(user)}";
            return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName }));
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
            return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName }));
        }

        return Unauthorized();
    }

    [HttpPost("[action]")]
    private async Task<bool> RegisterUser([FromBody] RegisterModelDto model)
    {
        var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), NationalCode = model.NationalCode, UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, PhoneNumber = model.Mobile, PhoneNumberConfirmed = true, PassWord = model.Password };

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
    public async Task<ConfirmResponse> ConfirmEmail(ConfirmRequest confirmRequest)
    {

        var user = userManager.FindByIdAsync(confirmRequest.UserId).Result;
        if (user == null)
        {
            return new ConfirmResponse { Errors = new List<string> { "user not found" }, IsSuccess = false };
        }

        var result = userManager.ConfirmEmailAsync(user, confirmRequest.Token).Result;
        if (result.Succeeded)
        {
            return new ConfirmResponse { IsSuccess = true };
        }
        return new ConfirmResponse { IsSuccess = false, Errors = result.Errors.Select(c => c.Description).ToList() };
    }

    [HttpPost("[action]")]
    public async Task<HttpResponseMessage> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
    {
        var user = userManager.FindByNameAsync(User.Identity.Name).Result;
        var setResult = userManager.SetPhoneNumberAsync(user, phoneNumberDto.PhoneNumber).Result;
        string code = userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumberDto.PhoneNumber).Result;

        return await smsService.SendSms(phoneNumberDto.PhoneNumber, code);
    }


    //[Authorize]
    [HttpPost("[action]")]
    public async Task<ConfirmResponse> VerifyPhoneNumber(VerifyPhoneNumberDto verify)
    {
        var user = userManager.FindByNameAsync(User.Identity.Name).Result;
        bool resultVerify = userManager.VerifyChangePhoneNumberTokenAsync(user, verify.Code, verify.PhoneNumber).Result;
        if (resultVerify == false)
        {

            return new ConfirmResponse { Errors = new List<string> { $"کد وارد شده برای شماره {verify.PhoneNumber} اشتباه است" }, IsSuccess = false };
        }
        else
        {
            user.PhoneNumberConfirmed = true;
            var resultUpdate = userManager.UpdateAsync(user).Result;
        }
        return new ConfirmResponse { IsSuccess = true };
    }



    private string GenerateJwtToken(ApplicationUser user)
    {
        var roles = userManager.GetRolesAsync(user).Result;

        var jwtSettings = configuration.GetSection("JWTSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["securityKey"]);

        var claims = new List<Claim>{
                                        new Claim(ClaimTypes.Name, user.UserName),
                                        new Claim(ClaimTypes.Email, user.Email),
                                        new Claim("NationalCode", user.NationalCode),
                                        new Claim("Mobile", user.PhoneNumber),
                                        new  System.Security.Claims.Claim("FullName",$"{user.FirstName} {user.LastName}"),
                                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                                    };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
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
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

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

        return tokenString;
    }

}







