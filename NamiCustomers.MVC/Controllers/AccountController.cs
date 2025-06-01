using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services.Account;
using NamiCustomers.MVC.Services.Auth.AuthServices;
using NamiCustomers.MVC.Services.Subscribers;
using System.Threading.Tasks;

namespace NamiCustomers.MVC.Controllers
{
    
    public class AccountController(IAccountService accountService,IAuthService authService) : Controller
    {
        //[Authorize]
        public async Task<IActionResult> Index()
        {
            var myAccount = await accountService.FindByNameAsync();
            return View(myAccount);
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
        public IActionResult Login(LoginDto login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var result = authService.LoginAsync(new Services.Auth.Dtos.LoginRequestDto { Email = login.UserName, Password = login.Password }).Result;


            if (result == true)
            {
                return Redirect(login.ReturnUrl);
            }


            ModelState.AddModelError(string.Empty, "Login  Error");
            return View();
        }
		[HttpGet]
		public IActionResult GetOtp()
		{
			return View();
		}



		[HttpPost]
		public IActionResult GetOtp(string mobile)
		{
			var result = accountService.GetOtp(mobile);
			return View();
		}

		//[HttpGet("[action]")]
		//public async Task<IActionResult> LogInByOtp([FromQuery] string otpCode)
		//{
		//	if (!string.IsNullOrEmpty(otpCode))
		//	{
		//		var otp = await subscriberService.SendOtp(otpCode);
		//		if (otp != null)
		//		{
		//			var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == otp.Data.Mobile).SingleOrDefaultAsync();
		//			if (user != null)
		//			{
		//				var token = $"{GenerateJwtToken(user.Email)}";
		//				return Ok(new { token });
		//			}
		//		}
		//		return Unauthorized();
		//	}
		//	return Unauthorized();
		//}

		//[Authorize]
		//public IActionResult TwoFactorEnabled()
		//{
		//    var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
		//    var Result = _userManager.SetTwoFactorEnabledAsync(user, !user.TwoFactorEnabled).Result;
		//    return RedirectToAction(nameof(Index));
		//}

		//public IActionResult Register()
		//{
		//    return View();
		//}
		//[HttpPost]
		//public IActionResult Register(RegisterDto register)
		//{
		//    if (ModelState.IsValid == false)
		//    {
		//        return View(register);
		//    }

		//    User newUser = new User()
		//    {
		//        FirstName = register.FirstName,
		//        LastName = register.LastName,
		//        Email = register.Email,
		//        UserName = register.Email,
		//        PassWord= register.Password
		//    };

		//    var result = _userManager.CreateAsync(newUser, register.Password).Result;
		//    if (result.Succeeded)
		//    {
		//        var token = _userManager.GenerateEmailConfirmationTokenAsync(newUser).Result;
		//        string callbackUrl = Url.Action("ConfirmEmail", "Account", new
		//        {
		//            UserId = newUser.Id
		//        ,
		//            token
		//        }, protocol: Request.Scheme);

		//        string body = $"لطفا برای فعال حساب کاربری بر روی لینک زیر کلیک کنید!  <br/> <a href={callbackUrl}> Link </a>";
		//        _emailService.Execute(newUser.Email, body, "فعال سازی حساب کاربری باگتو");

		//        return RedirectToAction("DisplayEmail");
		//    }

		//    string message = "";
		//    foreach (var item in result.Errors.ToList())
		//    {
		//        message += item.Description + Environment.NewLine;
		//    }
		//    TempData["Message"] = message;
		//    return View(register);
		//}
		//[Authorize(Roles = "Admin")]
		//public IActionResult ConfirmEmail(string UserId, string Token)
		//{
		//    if (UserId == null || Token == null)
		//    {
		//        return BadRequest();
		//    }
		//    var user = _userManager.FindByIdAsync(UserId).Result;
		//    if (user == null)
		//    {
		//        return View("Error");
		//    }

		//    var result = _userManager.ConfirmEmailAsync(user, Token).Result;
		//    if (result.Succeeded)
		//    {
		//        /// return 
		//    }
		//    else
		//    {

		//    }
		//    return RedirectToAction("login");

		//}
		//public IActionResult DisplayEmail()
		//{
		//    return View();
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

		//public IActionResult LogOut()
		//{
		//    _signInManager.SignOutAsync();
		//    return RedirectToAction("Index", "home");
		//}

		//public IActionResult ForgotPassword()
		//{
		//    return View();
		//}

		//[HttpPost]
		//public IActionResult ForgotPassword(ForgotPasswordConfirmationDto forgot)
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return View(forgot);
		//    }

		//    var user = _userManager.FindByEmailAsync(forgot.Email).Result;
		//    if (user == null || _userManager.IsEmailConfirmedAsync(user).Result == false)
		//    {
		//        ViewBag.meesage = "ممکن است ایمیل وارد شده معتبر نباشد! و یا اینکه ایمیل خود را تایید نکرده باشید";
		//        return View();
		//    }

		//    string token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
		//    string callbakUrl = Url.Action("ResetPassword", "Account", new
		//    {
		//        UserId = user.Id,
		//        token
		//    }, protocol: Request.Scheme);

		//    string body = $"برای تنظیم مجدد کلمه عبور بر روی لینک زیر کلیک کنید <br/> <a href={callbakUrl}> link reset Password </a>";
		//    _emailService.Execute(user.Email, body, "فراموشی رمز عبور");
		//    ViewBag.meesage = "لینک تنظیم مجدد کلمه عبور برای ایمیل شما ارسال شد";
		//    return View();
		//}

		//public IActionResult ResetPassword(string UserId, string Token)
		//{
		//    return View(new ResetPasswordDto
		//    {
		//        Token = Token,
		//        UserId = UserId,
		//    });
		//}

		//[HttpPost]
		//public IActionResult ResetPassword(ResetPasswordDto reset)
		//{
		//    if (!ModelState.IsValid)
		//        return View(reset);
		//    if (reset.Password != reset.ConfirmPassword)
		//    {
		//        return BadRequest();
		//    }
		//    var user = _userManager.FindByIdAsync(reset.UserId).Result;
		//    if (user == null)
		//    {
		//        return BadRequest();
		//    }

		//    var Result = _userManager.ResetPasswordAsync(user, reset.Token, reset.Password).Result;

		//    if (Result.Succeeded)
		//    {
		//        var currentUser= _userManager.FindByIdAsync(reset.UserId).Result;
		//        currentUser.PassWord = reset.Password;


		//      var result=  _userManager.UpdateAsync(currentUser).Result;

		//        return RedirectToAction(nameof(ResetPasswordConfirmation));
		//    }
		//    else
		//    {
		//        ViewBag.Errors = Result.Errors;
		//        return View(reset);
		//    }

		//}
		//[HttpGet]
		//public IActionResult ResetPasswordConfirmation()
		//{
		//    return View();
		//}


		//[Authorize]
		//public IActionResult SetPhoneNumber()
		//{
		//    return View();
		//}

		//[Authorize]
		//[HttpPost]
		//public IActionResult SetPhoneNumber(SetPhoneNumberDto phoneNumberDro)
		//{
		//    var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
		//    var setResult = _userManager.SetPhoneNumberAsync(user, phoneNumberDro.PhoneNumber).Result;
		//    string code = _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumberDro.PhoneNumber).Result;
		//    SmsService smsService = new SmsService();
		//    smsService.Send(phoneNumberDro.PhoneNumber, code);
		//    TempData["PhoneNumber"] = phoneNumberDro.PhoneNumber;
		//    return RedirectToAction(nameof(VerifyPhoneNumber));
		//}

		//[Authorize]
		//public IActionResult VerifyPhoneNumber()
		//{

		//    return View(new VerifyPhoneNumberDto
		//    {
		//        PhoneNumber = TempData["PhoneNumber"].ToString(),
		//    });
		//}

		//[Authorize]
		//[HttpPost]
		//public IActionResult VerifyPhoneNumber(VerifyPhoneNumberDto verify)
		//{
		//    var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
		//    bool resultVerify = _userManager.VerifyChangePhoneNumberTokenAsync(user, verify.Code, verify.PhoneNumber).Result;
		//    if (resultVerify == false)
		//    {
		//        ViewData["Message"] = $"کد وارد شده برای شماره {verify.PhoneNumber} اشتباه اشت";
		//        return View(verify);
		//    }
		//    else
		//    {
		//        user.PhoneNumberConfirmed = true;
		//        var resultUpdate = _userManager.UpdateAsync(user).Result;
		//    }
		//    return RedirectToAction("VerifySuccess");

		//}


		public IActionResult VerifySuccess()
        {
            return View();
        }


    }
}
