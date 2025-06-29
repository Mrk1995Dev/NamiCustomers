using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NamiCustomers.MVC.Models;
using NamiCustomers.MVC.Services.Account;
using NamiCustomers.MVC.Services.Auth;
using NuGet.Common;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace NamiCustomers.MVC.Controllers
{

    public class AccountController(IAccountService accountService, IAuthService authService, IUrlHelperFactory urlHelperFactory) : Controller
    {
        //[Authorize]
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
        public async Task<IActionResult> GetOtp(string mobile)
        {
            if (string.IsNullOrEmpty(mobile))
            {
                return RedirectToAction("LoginByMobile");
            }
            var code = await authService.GetOtp(mobile);

            TempData["mobile"] = mobile;
            return RedirectToAction("LoginByOtp");
        }
        [HttpGet]
        public async Task<IActionResult> LoginByOtp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginByOtp(string otp)
        {
            var result = await authService.LoginByOtpAsync(otp);
            if (result.Email != null)
            {
                var claims = new List<System.Security.Claims.Claim>
            {
                new  System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name,result.Email)
            };

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
            ModelState.AddModelError(string.Empty, "Login  Error");
            return View();
        }


        

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
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
                Token= "TEMPTOKEN"
            }, protocol: Request.Scheme);

            var result = await authService.ForgotPassword(new ForgotPasswordRequestDto {Email= forgot.Email,CallBAckUrl= callbakUrl });
            if (!result.Issuccess)
            {
                return ForgotPassword();
            }

            return View ("DisplayEmail");
        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string UserId ,string Token)
        {
            return View("ResetPassword", new ResetPasswordDto
            {
                UserId = UserId,
                Token = Token.Replace(" ","+")
            });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto reset)
        {
            var result=await authService.ResetPassword(reset);
            if (result.Issuccess)
            {
                return RedirectToAction(nameof(ResetPasswordConfirmation));
            }
            return  View("ResetPassword", new ResetPasswordDto
            {
                Errors=result.Errors.Errors.Select( c=>  c.Description).ToList(),
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
        public async Task<IActionResult> Register(RegisterDto register)
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

            var result= await authService.Register(register);
          
            if (result.IsSuccess)
            {
                return RedirectToAction("DisplayEmail");
            }

            string message = "";
            foreach (var error in result.Errors.ToList())
            {
                message +=$"{error} {Environment.NewLine}";
            }
            TempData["Message"] = message;
            return View(register);
        }
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmEmail(string UserId, string Token)
        {
            if (UserId == null || Token == null)
            {
                return BadRequest();
            }
           var result= await authService.ConfirmEmail(UserId,Token);
             
            if (!result.IsSuccess)
            {
                return RedirectToAction("Error",new ErrorViewModel {Errors=result.Errors.ToList() });
            }
            
            return RedirectToAction("login");
        }
        public IActionResult DisplayEmail()
        {
            return View();
        }
        


        public IActionResult SetPhoneNumber()
        {
            return View();
        }

        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> SetPhoneNumber(SetPhoneNumberDto phoneNumberDto)
        {

            await authService.SetPhoneNumber(phoneNumberDto);
             
            TempData["PhoneNumber"] = phoneNumberDto.PhoneNumber;
            return RedirectToAction(nameof(VerifyPhoneNumber));
        }

        //[Authorize]
        public IActionResult VerifyPhoneNumber()
        {

            return View(new VerifyPhoneNumberDto
            {
                PhoneNumber = TempData["PhoneNumber"].ToString(),
            });
        }

       // [Authorize]
        [HttpPost]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberDto verify)
        {
            var result = await authService.VerifyPhoneNumber(verify);
           
            if (result.IsSuccess == false)
            {
                ViewData["Message"] = result.Errors.Select(c=>c).ToList();
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
}
