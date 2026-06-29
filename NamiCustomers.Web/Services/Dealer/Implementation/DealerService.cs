using NamiCustomers.Web.Services.Common.Dto;
using NamiCustomers.Web.Services.Dealer.Contract;
using NamiCustomers.Web.Services.Dealer.Dto;
using System.Net.Http.Json;

namespace NamiCustomers.Web.Services.Dealer.Implementation
{
    public class DealerService(HttpClient httpClient) : IDealerService
    {
        public async Task<ResultDto<DealerResponseDto[]>> GetAllDealerAsync()
        {
            var result = new ResultDto<DealerResponseDto[]>("", false);
            var response = await httpClient.GetAsync("Dealer/GetDealers");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new ResultDto<DealerResponseDto[]>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };

            if(response.IsSuccessStatusCode)
                 return await response.Content.ReadFromJsonAsync<ResultDto<DealerResponseDto[]>>();

            else if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<DealerResponseDto[]>>();
                return new ResultDto<DealerResponseDto[]>(result.Message, result.Succeeded);
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<DealerResponseDto[]>>();
                return new ResultDto<DealerResponseDto[]>(result.Message, result.Succeeded);
            }

        }

        public async Task<ResultDto<BranchesByDealerResponse[]>> GetAllBranchesByDealerAsync(Guid dealerId)
        {
            var result = new ResultDto<BranchesByDealerResponse[]>("", false);
            var response = await httpClient.GetAsync($"Dealer/GetBranchesByDealer?dealerId={dealerId}");
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return new ResultDto<BranchesByDealerResponse[]>("", false)
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized
                };

            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ResultDto<BranchesByDealerResponse[]>>();

            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<BranchesByDealerResponse[]>>();
                return new ResultDto<BranchesByDealerResponse[]>(result.Message, result.Succeeded);
            }

            else
            {
                result = await response.Content.ReadFromJsonAsync<ResultDto<BranchesByDealerResponse[]>>();
                return new ResultDto<BranchesByDealerResponse[]>(result.Message, result.Succeeded);
            }
        }

    }
}
