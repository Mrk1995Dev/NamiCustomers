using Microsoft.AspNetCore.Identity;
using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;
using System.Net.Http.Json;
using System.Text.Json;

namespace NamiCustomers.MVC.Services;

public interface IUserService
{

    Task<ResultDto<List<UserDto>>> GetAllAsync();
    Task<ResultDto> RegisterAsync(RegisterDto register);
    Task<ResultDto> Edit(UserEditDto userEdit);
    Task<ResultDto> Remove(string id);
    Task<ResultDto> AddUserRole(AddUserRoleDto newRole);
    Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto);
    Task<ResultDto<UserDto>> GetAsync(string id);
    Task<ResultDto<AddUserRoleDto>> GetRolesAsync(string userId);
    
    Task<ResultDto> RemoveUserRole(AddUserRoleDto newRole);
}
public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    
    public UserService(HttpClient httpClient )
    {
        _httpClient = httpClient;
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

    public async Task<ResultDto<List<UserDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<UserDto>>>($"users/GetAll");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<List<UserDto>>(Infrastucture.Properties.Resources.errNotFound, false, null);
    }

    public async Task<ResultDto<UserDto>> GetAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<UserDto>>($"users/Get?id={id}");
        if (response.Succeeded)
        {
            return response;
        }
        return new ResultDto<UserDto>(Infrastucture.Properties.Resources.errNotFound, false, null);
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

    public async  Task<ResultDto> RemoveUserRole(AddUserRoleDto newRole)
    {
        var response = await _httpClient.PostAsJsonAsync($"users/RemoveUserRole",newRole);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgDeleted, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errDelete, false);
    }
}


