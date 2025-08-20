using System.Text.Json;

namespace NamiCustomers.Infrastucture.Utilities;

public static class RestUtility
{
    public static async Task<T> GetData<T>(string baseUrl, string apiAddress, dynamic queryString)
    {
        if (!string.IsNullOrEmpty(baseUrl))
        {
            apiAddress = $"{baseUrl}/{apiAddress}";
        }
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
        }
        return Activator.CreateInstance<T>();//TODO moradi
    }



    public static async Task<T> PostData<T>(string baseUrl, string apiAddress, dynamic queryModel)
    {
        if (!string.IsNullOrEmpty(baseUrl))
        {
            apiAddress = $"{baseUrl}/{apiAddress}";
        }
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Post, $"{apiAddress}");
        request.Headers.Add("Accept", "application/json");
        var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
        }
        else
        {
            return Activator.CreateInstance<T>()
            ;//TODO moradi
        }
            
    }
}
