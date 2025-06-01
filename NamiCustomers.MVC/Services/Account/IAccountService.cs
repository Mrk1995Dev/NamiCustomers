using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NamiCustomers.MVC.Services.Subscribers.Dtos;
using System.Reflection;

namespace NamiCustomers.MVC.Services.Account;

public interface IAccountService
{
    Task<MyAccountinfoDto> FindByNameAsync();
    Task<MyAccountinfoDto> GetOtp( string mobile);
}
public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;

    public AccountService(HttpClient httpClient)
    { 
        _httpClient = httpClient;
    }

    public async  Task<MyAccountinfoDto> FindByNameAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<MyAccountinfoDto>($"account/FindByNameAsync");
        return result;
    }

	public async Task<MyAccountinfoDto> GetOtp(string mobile)
	{
		var result = await _httpClient.GetFromJsonAsync<MyAccountinfoDto>($"account/GetOtp");
		return result;
	}


}


