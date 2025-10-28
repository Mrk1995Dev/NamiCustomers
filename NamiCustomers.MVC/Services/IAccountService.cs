using NamiCustomers.Abstractions.Dtos.Security.Dto;
using NamiCustomers.Infrastucture.Utilities;
using System.Text.Json;

namespace NamiCustomers.MVC.Services;

public interface IAccountService
{
    Task<MyAccountinfoDto> FindByNameAsync();

    Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto);
    Task<ResultDto<UserDto>> GetByNationalCodeAsync();

}
public class AccountService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor) : IAccountService
{
    public async Task<ResultDto<UserDto>> GetByNationalCodeAsync()
    {
        var nationalCode = httpContextAccessor.GetClaimValue(MyClaims.NationalCode);

        var response = await httpClient.GetAsync($"account/GetByNationalCode?nationalcode={nationalCode}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();
            return System.Text.Json.JsonSerializer.Deserialize<ResultDto<UserDto>>(content);
        }
        return ResultDto.Failure<UserDto>(response.ReasonPhrase);
    }
    public async Task<MyAccountinfoDto> FindByNameAsync()
    {
        var result = await httpClient.GetFromJsonAsync<MyAccountinfoDto>($"account/FindByName");
        return result;
    }



    public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync(MyAccountinfoDto myAccountinfoDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(myAccountinfoDto), null, "application/json");

        var res = await httpClient.PostAsync($"account/PasswordSignIn", content);
        return JsonSerializer.Deserialize<Microsoft.AspNetCore.Identity.SignInResult>(res.Content.ReadAsStream());
    }





}


