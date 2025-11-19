using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NamiCustomers.Infrastucture.ExternalServices.IranFava.Dtos;
using NamiCustomers.Infrastucture.Utilities;
using NamiCustomers.MVC.Services;
using NamiCustomers.MVC.Services.Auth;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NamiCustomers.MVC.Controllers;


public class AccountController(IAuthService authService, IUrlHelperFactory urlHelperFactory, IUserService userService,ICookieService cookieService) : MyBaseController
{
    
    public async Task<IActionResult> Index()
    {
        return View(User.Identity);
    }

    [HttpGet]

    public IActionResult Login(string returnUrl = "/")
    {

        return View(new LoginDto
        {
            ReturnUrl = returnUrl,
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        if (!ModelState.IsValid)
        {
            SetModelStateError();
            return View(login);
        }


        var result =await  authService.LoginAsync(new LoginRequestDto { Email = login.UserName, Password = login.Password });
        if (result)
        {
            var claims = new List<System.Security.Claims.Claim>
        {
            new  System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name,login.UserName)
        };

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }
        ModelState.AddModelError(string.Empty, "Login  Error");
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> LoginByMobile()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetOtp(string mobile, string nationalcode)
    {
        if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(nationalcode) || !mobile.IsValidIranianMobileNumber())
        {
            SetError(Infrastucture.Properties.Resources.errInputInValid);
            return RedirectToAction("LoginByMobile");
        }
        var result = await authService.GetOtpAsync(mobile, nationalcode);
        if (!result.Succeeded)
        {
            SetError(result.Message);
            return RedirectToAction("LoginByMobile");
        }


        return View("~/Views/Account/LoginByOtp.cshtml", ResultDto.Success<LoginResponseDto>(new LoginResponseDto { Mobile = mobile }));
    }
    [HttpGet]
    public async Task<IActionResult> LoginByOtp(string mobile)
    {
        if (!mobile.IsValidIranianMobileNumber())
        {
            SetError(Infrastucture.Properties.Resources.errInvalidMobile);
            return RedirectToAction("LoginByMobile");
        }
        return View("LoginByMobile.cshtml", new LoginResponseDto() { Mobile = mobile });
    }
    [HttpPost]
    public async Task<IActionResult> CheckOtp(string otp)
    {

        var result = await authService.LoginByOtpAsync(otp);
        if (!result.Succeeded)
        {
            SetError(result.Message);
            return View("~/Views/Account/LoginByMobile.cshtml");
        }
        else
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = (JwtSecurityToken)tokenHandler.ReadJwtToken(result.Data.Token);
            var claims = securityToken.Claims;

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



            // Check what claim type is used for roles in your JWT
            var roleClaims = claims.Where(c => c.Type == "role" || c.Type == ClaimTypes.Role).ToList();


            // If roles are stored in a custom claim type, add them as Role claims
            foreach (var claim in roleClaims)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, claim.Value));
            }

            //Note: For these changes to persist, you typically need to:
            //1.Sign out the user
            await HttpContext.SignOutAsync();

            // 2.Sign in with the modified principal
            await HttpContext.SignInAsync(
              CookieAuthenticationDefaults.AuthenticationScheme,
              new ClaimsPrincipal(claimsIdentity));
            //Save multiple data in cookies

        }

        return RedirectToAction("Index", "Home");
    }




    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
        await authService.LogoutAsync();
        return RedirectToAction("LoginByMobile");
    }
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }
    public IActionResult AccessDenied()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest forgot)
    {
        var urlHelper = urlHelperFactory.GetUrlHelper(ControllerContext);

        string callbakUrl = urlHelper.Action("ResetPassword", "Account", new
        {
            UserId = forgot.Email,
            Token = "TEMPTOKEN"
        }, protocol: Request.Scheme);

        var result = await authService.ForgotPasswordAsync(new ForgotPasswordRequestDto { Email = forgot.Email, CallBAckUrl = callbakUrl });
        if (!result.Succeeded)
        {
            return ForgotPassword();
        }

        return View("DisplayEmail");
    }

    [HttpGet]
    public async Task<IActionResult> ResetPassword(string UserId, string Token)
    {
        return View("ResetPassword", new ResetPasswordDto
        {
            UserId = UserId,
            Token = Token.Replace(" ", "+")
        });
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto reset)
    {
        var result = await authService.ResetPasswordAsync(reset);
        if (result.Succeeded)
        {
            return RedirectToAction(nameof(ResetPasswordConfirmation));
        }
        return View("ResetPassword", new ResetPasswordDto
        {
            Errors = result.Errors.Select(c => c).ToList(),
            UserId = reset.UserId,
            Token = reset.Token.Replace(" ", "+")
        });

    }



    [HttpGet]
    public IActionResult ResetPasswordConfirmation()
    {
        return View();
    }



    public IActionResult VerifySuccess()
    {
        return View();
    }


    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserDto register)
    {
        if (!ModelState.IsValid)
        {
            SetModelStateError();
            return View(register);
        }
        var urlHelper = urlHelperFactory.GetUrlHelper(ControllerContext);
        string callbakUrl = urlHelper.Action("ResetPassword", "Account", new
        {
            Token = "TEMPTOKEN"
        }, protocol: Request.Scheme);


        register.CallbakUrl = callbakUrl;

        var result = await authService.RegisterAsync(register);

        if (result.Succeeded)
        {
            return RedirectToAction("DisplayEmail");
        }

        string message = "";
        //foreach (var error in result.ErrorResponse.Errors.ToList())//todo moradi
        //{
        //    message += $"{error} {Environment.NewLine}";
        //}
        TempData["Message"] = message;
        return View(register);
    }
    [Authorize(Policy = nameof(MyPloicies.AdminAccess))]
    public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
    {
        if (UserId == null || Token == null)
        {
            return BadRequest();
        }
        var result = await authService.ConfirmEmailAsync(UserId, Token);
        if (result.IsSuccess == false)
        {
            ViewData["Message"] = result.Errors.Select(c => c).ToList();
            return RedirectToAction("Error", new ErrorViewModel { Errors = result.Errors.ToList() });
        }

        return RedirectToAction("VerifySuccess");


    }


    public IActionResult DisplayEmail()
    {
        return View();
    }


    [Authorize]
    public IActionResult SetPhoneNumber()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
    {

        await authService.SetPhoneNumberAsync(phoneNumberDto);

        TempData["PhoneNumber"] = phoneNumberDto.PhoneNumber;
        return RedirectToAction(nameof(VerifyPhoneNumber));
    }
    [Authorize]
    public IActionResult VerifyPhoneNumber()
    {

        return View(new VerifyPhoneNumberDto
        {
            PhoneNumber = TempData["PhoneNumber"].ToString(),
        });
    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberDto verify)
    {
        var result = await authService.VerifyPhoneNumberAsync(verify);

        if (result.IsSuccess == false)
        {
            ViewData["Message"] = result.Errors.Select(c => c).ToList();
            return View(verify);
        }

        return RedirectToAction("VerifySuccess");

    }

    #region TwoFactor
    //[Authorize]
    //public IActionResult TwoFactorEnabled()
    //{
    //    var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
    //    var Result = _userManager.SetTwoFactorEnabledAsync(user, !user.TwoFactorEnabled).Result;
    //    return RedirectToAction(nameof(Index));
    //}

    //public IActionResult TwoFactorLogin(string UserName, bool IsPersistent)
    //{
    //    var user = _userManager.FindByNameAsync(UserName).Result;
    //    if (user == null)
    //    {
    //        return BadRequest();
    //    }

    //    var providers = _userManager.GetValidTwoFactorProvidersAsync(user).Result;

    //    TwoFactorLoginDto model = new TwoFactorLoginDto();
    //    if (providers.Contains("Phone"))
    //    {
    //        string smsCode = _userManager.GenerateTwoFactorTokenAsync(user, "Phone").Result;

    //        SmsService smsService = new SmsService();
    //        smsService.Send(user.PhoneNumber, smsCode);
    //        model.Provider = "Phone";
    //        model.IsPersistent = IsPersistent;

    //    }
    //    else if (providers.Contains("Email"))
    //    {
    //        string emailCode = _userManager.GenerateTwoFactorTokenAsync(user, "Email").Result;
    //        EmailService emailService = new EmailService();
    //        emailService.Execute(user.Email, $"Two Factor Code:{emailCode}", "Two Factor Login");

    //        model.Provider = "Email";
    //        model.IsPersistent = IsPersistent;
    //    }


    //    return View(model);

    //}

    //[HttpPost]
    //public IActionResult TwoFactorLogin (TwoFactorLoginDto twoFactor)
    //{
    //    if(!ModelState.IsValid)
    //    {
    //        return View(twoFactor);
    //    }

    //    var user = _signInManager.GetTwoFactorAuthenticationUserAsync().Result;
    //    if(user == null)
    //    {
    //        return BadRequest();
    //    }

    //    var result = _signInManager.TwoFactorSignInAsync(twoFactor.Provider, twoFactor.Code, twoFactor.IsPersistent, false).Result;

    //    if(result.Succeeded)
    //    {
    //        return RedirectToAction("index");
    //    }
    //    else if(result.IsLockedOut)
    //    {
    //        ModelState.AddModelError("", "حساب کاربری شما قفل شده است");
    //        return View();
    //    }
    //    else
    //    {
    //        ModelState.AddModelError("", "کد وارد شده صحیح نیست ");
    //        return View();
    //    }
    //}



    //[Authorize]
    #endregion
}
