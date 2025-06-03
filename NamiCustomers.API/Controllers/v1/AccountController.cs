using k8s.KubeConfigModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NamiCustomers.Application.Services.Subscribers;
using NamiCustomers.Domain.Entities.Account;
using NamiCustomers.Infrastucture.Model.Account;
using NamiCustomers.Infrastucture.Utilities;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NamiCustomers.API.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]

public class AccountController(IConfiguration configuration, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, ISubscriberService subscriberService
    ) : ControllerBase
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
    

    [HttpGet("[action]")]
    public async  Task<IActionResult> GetCurrentUser()
    {
        return Ok(new ResultDto<List<ClaimsIdentity>>("", true, User.Identities.ToList()));
    }

        [HttpPost("[action]")]
    public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto)
    {
        await signInManager.SignOutAsync();
        var user = new ApplicationUser()
        {
            Email = myAccountinfoDto.Email,
            EmailConfirmed = myAccountinfoDto.EmailConfirmed,
            FullName = myAccountinfoDto.FullName,
            Id = myAccountinfoDto.Id,
            PhoneNumber = myAccountinfoDto.PhoneNumber,
            PhoneNumberConfirmed = myAccountinfoDto.PhoneNumberConfirmed,
            TwoFactorEnabled = myAccountinfoDto.TwoFactorEnabled,
            UserName = myAccountinfoDto.UserName,
        };

        var a = User.Identity;
        return await signInManager.PasswordSignInAsync(user, myAccountinfoDto.Password, myAccountinfoDto.IsPersistent, true);

    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetOtp([FromQuery] string mobile)
    {
        var result = await subscriberService.GetOtp(mobile);
        return Ok(result);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> LogInByOtp([FromQuery] string otpCode)
    {
        if (!string.IsNullOrEmpty(otpCode))
        {
            var otp = await subscriberService.SendOtp(otpCode);
            if (otp != null)
            {
                var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == otp.Data.Mobile).SingleOrDefaultAsync();
                if (user != null)
                {
                    var token = $"{GenerateJwtToken(user)}";
                    var refreshToken= $"{GenerateJwtToken(user)}";
                    return Ok(new ResultDto<LoginResponseDto>("",true,new LoginResponseDto { RefreshToken= refreshToken, Token=token,Email=user.Email}));
                }
            }
            return Unauthorized();
        }
        return Unauthorized();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LogIn(LoginModel model)
    {
        var user = userManager.FindByNameAsync(model.Email).Result;
        await signInManager.SignOutAsync();
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.IsPersistent, false);
        if (result.Succeeded)
        {
            var token = $"{GenerateJwtToken(user)}";
            var refreshToken = $"{GenerateJwtToken(user)}";
            return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email }));
        }
        return Unauthorized();
    }



    [HttpGet("[action]")]
    public async Task<IActionResult> GetToken([FromQuery] LoginModel model)
    {
        if (!string.IsNullOrEmpty(model.Mobile))
        {
            var user = await userManager.Users.WhereIf(true, c => c.PhoneNumber == model.Mobile).SingleOrDefaultAsync();
            if (user != null)
            {
                var token = $"{GenerateJwtToken(user)}";
                var refreshToken = $"{GenerateJwtToken(user)}";
                return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email }));
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
            return Ok(new ResultDto<LoginResponseDto>("", true, new LoginResponseDto { RefreshToken = refreshToken, Token = token, Email = user.Email }));
        }

        return Unauthorized();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FullName = model.FullName, PhoneNumber = model.Mobile, PhoneNumberConfirmed = true };
        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return Ok(new { message = "User registered successfully!" });
        }

        return BadRequest(result.Errors);
    }

    private string GenerateJwtToken(ApplicationUser user)
    {
        var roles = userManager.GetRolesAsync(user).Result;

        var jwtSettings = configuration.GetSection("JWTSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["securityKey"]);

        var claims = new List<Claim>
{
    new Claim(ClaimTypes.Name, user.UserName),
    new Claim(ClaimTypes.Email, user.Email),
    new Claim(ClaimTypes.NameIdentifier, user.Id)
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
        return tokenHandler.WriteToken(token);
    }

}


public class LoginModel
{

    public string Email { get; set; }

    public string Password { get; set; }
    public string Mobile { get; set; }
    public bool IsPersistent { get; set; } = false;

}


public class RegisterModel
{
    public string FullName { get; set; }
    public string Mobile { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}

