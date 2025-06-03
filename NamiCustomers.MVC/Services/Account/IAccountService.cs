using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.Infrastucture.Model.Account;
using NamiCustomers.MVC.Services.Auth;
using NamiCustomers.MVC.Services.Subscribers;
using NamiCustomers.MVC.Services.Subscribers.Dtos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace NamiCustomers.MVC.Services.Account;

public interface IAccountService
{
    Task<MyAccountinfoDto> FindByNameAsync();
 
    Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync( MyAccountinfoDto myAccountinfoDto);
 
}
public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
 

    public AccountService(HttpClient httpClient )
    { 
        _httpClient = httpClient;
 
    }

    public async  Task<MyAccountinfoDto> FindByNameAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<MyAccountinfoDto>($"account/FindByName");
        return result;
    }

   

    public async Task<Microsoft.AspNetCore.Identity.SignInResult> PasswordSignInAsync( MyAccountinfoDto myAccountinfoDto)
    {
        var content = new StringContent(JsonSerializer.Serialize(myAccountinfoDto), null, "application/json");

        var  res=  await _httpClient.PostAsync($"account/PasswordSignIn", content);
        return JsonSerializer.Deserialize<Microsoft.AspNetCore.Identity.SignInResult>(res.Content.ReadAsStream());
    }

 



}


