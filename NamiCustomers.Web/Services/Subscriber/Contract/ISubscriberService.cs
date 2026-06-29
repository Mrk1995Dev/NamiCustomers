using NamiCustomers.Abstractions.Dtos;
using NamiCustomers.Web.Services.Subscriber.Dto;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Subscriber.Contract
{
    public interface ISubscriberService
    {
        Task<ResultDto<SubscriberDto>> GetSubscriberInfoAsync();
    }

    public class SubscriberService(HttpClient httpClient) : ISubscriberService
    {
        public async Task<ResultDto<SubscriberDto>> GetSubscriberInfoAsync()
        {
            var result = new ResultDto<SubscriberDto>("", false);
            var response = await httpClient.GetAsync("Subscriber/InfoByNationalCode");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new ResultDto<SubscriberDto>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };

            else if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResultDto<SubscriberDto>>();
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<SubscriberDto>>();
                return new ResultDto<SubscriberDto>(result.Message, result.Succeeded);
            }
        }
    }
}
