using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;

namespace NamiCustomers.MVC.Services;

public interface IRoleService
{
    Task<ResultDto<List<RoleListDto>>> GetAllAsync();
    Task<ResultDto> RegisterAsync(AddNewRoleDto role);
    Task<ResultDto<List<UserListDto>>> GetUsersInRole(string Name);
    Task<ResultDto<RoleListDto>> GetAsync(string id);
    Task<ResultDto> Edit(RoleListDto roleEdit);
}
public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;


    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResultDto> Edit(RoleListDto  roleEdit)
    {
        var response = await _httpClient.PutAsJsonAsync($"roles/edit", roleEdit);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
    }

    public async Task<ResultDto<List<RoleListDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<RoleListDto>>>($"Roles/GetAll");

        if (response.Succeeded)
        {
            return new ResultDto<List<RoleListDto>>(
                 Infrastucture.Properties.Resources.msgFound,
                true,
                response.Data)
            {

            };
        }

        return new ResultDto<List<RoleListDto>>(
           Infrastucture.Properties.Resources.errNotFound,
            false,
            null);
    }

    public async  Task<ResultDto<RoleListDto>> GetAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<RoleListDto>>($"Roles/Get?id={id}");

        if (response.Succeeded)
        {
            return new ResultDto<RoleListDto>(
                 Infrastucture.Properties.Resources.msgFound,
                true,
                response.Data)
            {

            };
        }

        return new ResultDto<RoleListDto>(
           Infrastucture.Properties.Resources.errNotFound,
            false,
            null);
    }

    public async Task<ResultDto<List<UserListDto>>> GetUsersInRole(string roleName)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<UserListDto>>>($"Roles/GetUsersInRole?name={roleName}");
        if (response.Succeeded)
        {
            return new ResultDto<List<UserListDto>>(
                 Infrastucture.Properties.Resources.msgSave,
                true,
                response.Data);
        }

        return new ResultDto<List<UserListDto>>(
           Infrastucture.Properties.Resources.errSave,
            false,
            null);
    }

    public async Task<ResultDto> RegisterAsync(AddNewRoleDto role)
    {
        var response = await _httpClient.PostAsJsonAsync($"Roles/register", role);
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
}


