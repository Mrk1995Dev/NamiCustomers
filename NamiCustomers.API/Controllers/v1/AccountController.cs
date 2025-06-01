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
    [HttpGet("[action]")]
    public async Task<MyAccountinfoDto> FindByNameAsync()
    {
        var user = userManager.FindByNameAsync(User.Identity.Name).Result;
        var myAccount = new MyAccountinfoDto()
        {
            Email = user.Email,
            EmailConfirmed = user.EmailConfirmed,
            FullName = $"{user.FirstName} {user.LastName}",
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            PhoneNumberConfirmed = user.PhoneNumberConfirmed,
            TwoFactorEnabled = user.TwoFactorEnabled,
            UserName = user.UserName,
        };
        return myAccount;
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
                    var token = $"{GenerateJwtToken(user.Email)}";
                    return Ok(new { token });
                }
            }
            return Unauthorized();
        }
        return Unauthorized();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> LogIn(LoginModel model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var token = $"{GenerateJwtToken(model.Email)}";
            return Ok(new { token });
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
                var token = $"{GenerateJwtToken(user.Email)}";
                return Ok(new { token });
            }
            return Unauthorized();
        }

        //a.moradi@namikhodro.com Aa12334566*
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (result.Succeeded)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            var token = $"{GenerateJwtToken(model.Email)}";
            return Ok(new { token });
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

    private string GenerateJwtToken(string username)
    {
        var jwtSettings = configuration.GetSection("JWTSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["securityKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }),
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

