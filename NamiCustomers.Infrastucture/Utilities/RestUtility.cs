using System.Text.Json;

namespace NamiCustomers.Infrastucture.Utilities;

public static class RestUtility
{
    public static async Task<T> GetData<T>(string baseUrl, string apiAddress, dynamic queryString)
    {
        try
        {

         
        if (!string.IsNullOrEmpty(baseUrl))
        {
            apiAddress = $"{baseUrl}{apiAddress}";
        }
        using HttpClient client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");
        request.Headers.Add("Accept", "application/json");
        var response = await client.SendAsync(request);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var a= await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
                return a;
        }
            // Create detailed error message
            var errorMessage = $"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}";
            if (!string.IsNullOrEmpty(responseContent))
            {
                errorMessage += $"\nResponse: {responseContent}";
            }

            throw new HttpRequestException(errorMessage, null, response.StatusCode);
        }
        catch (HttpRequestException ex)
        {
            // Re-throw HTTP specific exceptions
            throw ex;
        }
        catch (JsonException ex)
        {
            throw new Exception($"Failed to deserialize response: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Unexpected error: {ex.Message}", ex);
        }
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
