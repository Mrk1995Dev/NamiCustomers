using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Account;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using NamiCustomers.Domain.Entities.Account;

namespace NamiCustomers.API.Controllers.v1
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;

        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var users =await  userManager.Users
                .Select(p => new UserListDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    EmailConfirmed = p.EmailConfirmed,
                    AccessFailedCount = p.AccessFailedCount
                }).ToListAsync();
            return Ok(users);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Create(RegisterDto register)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(register);
            }

            ApplicationUser newUser = new ApplicationUser()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,
                PassWord=register.Password
                ,EmailConfirmed=true
            };

            var result =await userManager.CreateAsync(newUser, register.Password);
            if (result.Succeeded)
            {
                Ok(result);
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            //TempData["Message"] = message;
            return BadRequest(register);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(UserEditDto userEdit)
        {
            var user =await  userManager.FindByIdAsync(userEdit.Id);
            user.FirstName = userEdit.FirstName;
            user.LastName = userEdit.LastName;
            user.PhoneNumber = userEdit.PhoneNumber;
            user.Email = userEdit.Email;
            user.UserName = userEdit.UserName;
            user.EmailConfirmed = userEdit.EmailConfirmed;
           var result=await   userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return Ok(result);
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            //TempData["Message"] = message;
            return BadRequest(result);
        }


        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(UserDeleteDto  userDelete)
        {
            var user = userManager.FindByIdAsync(userDelete.Id).Result;

           var result=  userManager.DeleteAsync(user).Result;

            if(result.Succeeded)
            {
                return Ok(result);
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            //TempData["Message"] = message;

            return BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserRole(AddUserRoleDto newRole)
        {
            var user =await  userManager.FindByIdAsync(newRole.Id);
            var result =await  userManager.AddToRoleAsync(user, newRole.Role);
            return Ok(user);
        }
    }
}
