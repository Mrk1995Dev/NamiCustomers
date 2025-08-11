using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NamiCustomers.Infrastucture.Utilities
{
    public static class RestUtility
    {
        public static async Task<T> GetData<T>(string baseUrl, string apiAddress, dynamic queryString)
        {
            if (!string.IsNullOrEmpty( baseUrl))
            {
                apiAddress = $"{baseUrl}/{apiAddress}";
            }
            using HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");
            request.Headers.Add("Accept", "application/json");
            var response = await client.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            return await Task.FromResult(JsonSerializer.Deserialize<T>(content));
        }



        public static async Task<T> PostData<T>(string apiAddress, dynamic queryModel)
        {
            string baseUrl = Infrastucture.Properties.Resource7Soft.BaseUrl;
            using HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/{apiAddress}");
            request.Headers.Add("Accept", "application/json");
            var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            string responseContent = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
        }
    }
}
