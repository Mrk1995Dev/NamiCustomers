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
            return ResultDto.Success(Infrastucture.Properties.Resources.msgEdited);
        }

        return ResultDto.Failure(Infrastucture.Properties.Resources.errEdited);
    }

    public async Task<ResultDto<List<RoleDto>>> GetAllAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<RoleDto>>>($"Roles/GetAll");

        if (response.Succeeded)
        {
            return   ResultDto.Success  <List<RoleDto>>(  response.Data);
        }

        return   ResultDto.Failure<List<RoleDto>>(
           Infrastucture.Properties.Resources.errNotFound);
    }

    public async Task<ResultDto<RoleDto>> GetAsync(string id)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<RoleDto>>($"Roles/Get?id={id}");

        if (response.Succeeded)
        {
            return ResultDto.Success<RoleDto>(response.Data);
         
        }

        return   ResultDto.Failure<RoleDto>(
           Infrastucture.Properties.Resources.errNotFound 
            );
    }

    public async Task<ResultDto<List<UserDto>>> GetUsersInRole(string roleName)
    {
        var response = await _httpClient.GetFromJsonAsync<ResultDto<List<UserDto>>>($"Roles/GetUsersInRole?name={roleName}");
        if (response.Succeeded)
        {
            return   ResultDto.Success<List<UserDto>>(  response.Data);
        }

        return   ResultDto.Failure<List<UserDto>>(
           Infrastucture.Properties.Resources.errSave 
           );
    }

    public async Task<ResultDto> RegisterAsync(AddNewRoleDto role)
    {
        var response = await _httpClient.PostAsJsonAsync($"Roles/register", role);
        if (response.IsSuccessStatusCode)
        {
            return   ResultDto.Success(
                 Infrastucture.Properties.Resources.msgSave );
        }

        return   ResultDto.Failure( Infrastucture.Properties.Resources.errSave);
    }
}


