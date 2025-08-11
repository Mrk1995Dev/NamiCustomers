using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Abstractions.Dtos.Security.Dto.Roles;

namespace NamiCustomers.MVC.Services;

public interface IRoleService
{
    Task<ResultDto<List<RoleDto>>> GetAllAsync();
    Task<ResultDto> RegisterAsync(AddNewRoleDto role);
    Task<ResultDto<List<UserDto>>> GetUsersInRole(string Name);
    Task<ResultDto<RoleDto>> GetAsync(string id);
    Task<ResultDto> Edit(RoleDto roleEdit);
}
public class RoleService : IRoleService
{
    private readonly HttpClient _httpClient;


    public RoleService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResultDto> Edit(RoleDto roleEdit)
    {
        var response = await _httpClient.PutAsJsonAsync($"roles/edit", roleEdit);
        if (response.IsSuccessStatusCode)
        {
            return new ResultDto(Infrastucture.Properties.Resources.msgEdited, true);
        }

        return new ResultDto(Infrastucture.Properties.Resources.errEdited, false);
    }

    public async Task<ResultDto<List<RoleDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<RoleDto>>>($"Roles/GetAll");

        if (response.Succeeded)
        {
            return new ResultDto<List<RoleDto>>(
                 Infrastucture.Properties.Resources.msgFound
               , true,
                response.Data)
            {

            };
        }

        return new ResultDto<List<RoleDto>>(
           Infrastucture.Properties.Resources.errNotFound, false);
    }

    public async Task<ResultDto<RoleDto>> GetAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<RoleDto>>($"Roles/Get?id={id}");

        if (response.Succeeded)
        {
            return new ResultDto<RoleDto>(
                 Infrastucture.Properties.Resources.msgFound,
            true,
                response.Data)
            {

            };
        }

        return new ResultDto<RoleDto>(
           Infrastucture.Properties.Resources.errNotFound, false
            );
    }

    public async Task<ResultDto<List<UserDto>>> GetUsersInRole(string roleName)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<UserDto>>>($"Roles/GetUsersInRole?name={roleName}");
        if (response.Succeeded)
        {
            return new ResultDto<List<UserDto>>(
                 Infrastucture.Properties.Resources.msgSave
              , true,
                response.Data);
        }

        return new ResultDto<List<UserDto>>(
           Infrastucture.Properties.Resources.errSave, false
           );
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


