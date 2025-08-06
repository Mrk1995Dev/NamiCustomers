using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NamiCustomers.MVC.Services;
using NamiCustomers.MVC.Services.Auth;
using Newtonsoft.Json;
using System.Reflection;
using System.Security.Claims;

namespace NamiCustomers.MVC.Controllers;


public class AccountController(IAuthService authService, IUrlHelperFactory urlHelperFactory, IUserService userService) : MyBaseController
{
    [Authorize]
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
            return View(login);
        }


        var result = authService.LoginAsync(new LoginRequestDto { Email = login.UserName, Password = login.Password }).Result;
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
        if (string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(nationalcode))
        {
            return RedirectToAction("LoginByMobile");
        }
        var code = await authService.GetOtpAsync(mobile, nationalcode);


        return View("~/Views/Account/LoginByOtp.cshtml",new ResultDto<LoginResponseDto>("",true, new LoginResponseDto { Mobile=mobile}));
    }
    [HttpGet]
    public async Task<IActionResult> LoginByOtp(string mobile)
    {
        return View("LoginByMobile.cshtml", new LoginResponseDto() {Mobile=mobile });
    }
    [HttpPost]
    public async Task<IActionResult> CheckOtp(string otp)
    {
        var result = await authService.LoginByOtpAsync(otp);
        if (!result.Succeeded)
        {
            return View("~/Views/Account/LoginByOtp.cshtml", new ResultDto<LoginResponseDto>(result.Message, false,result.Data));
        }
        var user = result.Data;
        var userRoles = (await userService.GetRolesAsync(user.Id)).Data;
        if (user.Email != null)
        {
            var claims = new List<System.Security.Claims.Claim>
        {
            new  System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name,user.Email),
            new  System.Security.Claims.Claim("NationalCode",user.NationalCode),
            new  System.Security.Claims.Claim("Mobile",user.Mobile),
            new  System.Security.Claims.Claim("UserId",user.Id),
            new  System.Security.Claims.Claim("FullName",$"{user.FirstName} {user.LastName}"),
        };
            foreach (var role in userRoles.Roles)
            {
                claims.Add(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role.Name));
                claims.Add(new System.Security.Claims.Claim("PersianRole", role.Description));
            }

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            //var us = await authService.GetCurrentUserAsync();
            //ClaimsPrincipal claimsPrincipal=new ClaimsPrincipal(us);
            // Note: For these changes to persist, you typically need to:
            // 1. Sign out the user
            // await HttpContext.SignOutAsync();

            // 2. Sign in with the modified principal
            // await HttpContext.SignInAsync(claimsPrincipal);

            return RedirectToAction("Index", "Home");
        }
        //TempData["otpError"] = "Login  Error";
        return View();
    }




    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear();
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
        if (ModelState.IsValid == false)
        {
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
