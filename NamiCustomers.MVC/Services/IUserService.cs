using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using System.Text.Json;

namespace NamiCustomers.MVC.Services;

public interface IUserService
{

    Task<ResultDto<List<UserListDto>>> GetAllAsync();
    Task<ResultDto> RegisterAsync(RegisterDto register);
    Task<ResultDto> Edit(UserEditDto userEdit);
    Task<ResultDto> Remove(string id);
    Task<ResultDto> AddUserRole(AddUserRoleDto newRole);
    Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto);
    Task<ResultDto<UserListDto>> GetAsync(string id);
    Task<ResultDto<AddUserRoleDto>> GetRolesAsync(string userId);
}
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        this._httpContextAccessor = httpContextAccessor;
    }
 

    public async Task<ResultDto> Edit(UserEditDto userEdit)
    {
        var response = await _httpClient.PutAsJsonAsync($"users/edit", userEdit);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
    }

    public async Task<ResultDto<List<UserListDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<UserListDto>>>($"users/GetAll");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<List<UserListDto>>(Infrastucture.Properties.Resources.errNotFound, false, null);
    }

    public async Task<ResultDto<UserListDto>> GetAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<UserListDto>>($"users/Get?id={id}");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<UserListDto>(Infrastucture.Properties.Resources.errNotFound, false, null);
    }

    public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(myAccountinfoDto), null, "application/json");

        var res = await _httpClient.PostAsync($"account/PasswordSignIn", content);
        return JsonSerializer.Deserialize<Microsoft.AspNetCore.Identity.SignInResult>(res.Content.ReadAsStream());
    }

    public async  Task<ResultDto> RegisterAsync(RegisterDto register)
    {
        var response = await _httpClient.PostAsJsonAsync($"users/register", register);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(
                 Infrastucture.Properties.Resources.msgSave,
                true);
        }

        return new ResultDto(
           Infrastucture.Properties.Resources.errSave,
            false);
    }

    public async Task<ResultDto> Remove(string id)
    {
        var response = await _httpClient.DeleteAsync($"users/remove?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgDeleted, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errDelete, false);
    }

    public async Task<ResultDto> AddUserRole(AddUserRoleDto  addUserRoleDto)
    {
        var response = await _httpClient.PostAsJsonAsync($"users/AddUserRole", addUserRoleDto);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(
                 Infrastucture.Properties.Resources.msgSave,
                true);
        }

        return new ResultDto(
           Infrastucture.Properties.Resources.errSave,
            false);
    }

    public async Task<ResultDto<AddUserRoleDto>> GetRolesAsync(string userId)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<AddUserRoleDto>>($"users/GetUserRoles?userId={userId}");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<AddUserRoleDto>(Infrastucture.Properties.Resources.errNotFound, false, null);
    }
}


