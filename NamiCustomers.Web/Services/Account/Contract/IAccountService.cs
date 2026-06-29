using NamiCustomers.Web.Services.Account.Dto;
using NamiCustomers.Web.Services.Common.Dto;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Account.Contract
{
    public interface IAccountService
    {
        Task<ResultDto> ResetPasswordAsync(ResetPasswordDto request);
    }

    public class AccountService(HttpClient httpClient) : IAccountService
    {
        public async Task<ResultDto> ResetPasswordAsync(ResetPasswordDto request)
        {
            var response = await httpClient.PostAsJsonAsync("Account/ResetPassword", request);

            if(response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResultDto>();
                return result;
            }

            else
            {
                //var result = await response.Content.ReadAsStringAsync();
                var result = await response.Content.ReadFromJsonAsync<ResultDto>();
                return result;
            }
        }
    }
}