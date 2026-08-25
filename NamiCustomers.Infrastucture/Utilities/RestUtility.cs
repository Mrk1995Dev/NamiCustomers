using System.Net.Http.Headers;
using System.Text.Json;
using NamiCustomers.Infrastucture.ExternalServices.SevenSoft.Dtos;
using NamiCustomers.Infrastucture.Properties;

namespace NamiCustomers.Infrastucture.Utilities;

public static class RestUtility
{
    private static readonly SemaphoreSlim TokenLock = new(1, 1);
    private static string? CachedToken;
    private static DateTime TokenExpiresAtUtc = DateTime.MinValue;

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
            await AttachTokenAsync(request);

            var response = await client.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ClearToken();
                request = new HttpRequestMessage(HttpMethod.Get, $"{apiAddress}{queryString}");
                request.Headers.Add("Accept", "application/json");
                await AttachTokenAsync(request);
                response = await client.SendAsync(request);
            }

            string responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var a = await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
                return a;
            }

            var errorMessage = $"HTTP Error: {(int)response.StatusCode} - {response.ReasonPhrase}";
            if (!string.IsNullOrEmpty(responseContent))
            {
                errorMessage += $"\nResponse: {responseContent}";
            }

            throw new HttpRequestException(errorMessage, null, response.StatusCode);
        }
        catch (HttpRequestException)
        {
            throw;
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
        await AttachTokenAsync(request);
        var content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
        request.Content = content;
        var response = await client.SendAsync(request);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            ClearToken();
            request = new HttpRequestMessage(HttpMethod.Post, $"{apiAddress}");
            request.Headers.Add("Accept", "application/json");
            await AttachTokenAsync(request);
            request.Content = new StringContent(JsonSerializer.Serialize(queryModel), null, "application/json");
            response = await client.SendAsync(request);
        }

        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return await Task.FromResult(JsonSerializer.Deserialize<T>(responseContent));
        }

        return Activator.CreateInstance<T>();
    }

    public static async Task<T> Authenticate<T>(LoginAuthenticateRequest? request = null)
    {
        var json = await AuthenticateRawAsync(request);
        return JsonSerializer.Deserialize<T>(json)!;
    }

    public static async Task<string> GetTokenAsync()
    {
        if (HasValidToken())
            return CachedToken!;

        await TokenLock.WaitAsync();
        try
        {
            if (HasValidToken())
                return CachedToken!;

            var json = await AuthenticateRawAsync();
            CachedToken = ExtractToken(json);
            TokenExpiresAtUtc = DateTime.UtcNow.AddMinutes(25);
            return CachedToken ?? string.Empty;
        }
        finally
        {
            TokenLock.Release();
        }
    }

    private static async Task AttachTokenAsync(HttpRequestMessage request)
    {
        var token = await GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            return;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    private static async Task<string> AuthenticateRawAsync(LoginAuthenticateRequest? request = null)
    {
        request ??= new LoginAuthenticateRequest();

        var baseUrl = (Resource7Soft.BaseUrlCrm ?? "http://172.20.10.6:8088").TrimEnd('/');
        var url = $"{baseUrl}/api/Login/Authenticate";

        using var client = new HttpClient();
        var httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
        httpRequest.Headers.Add("Accept", "application/json");
        httpRequest.Content = new StringContent(JsonSerializer.Serialize(request), null, "application/json");

        var response = await client.SendAsync(httpRequest);
        var json = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Login failed: {(int)response.StatusCode} - {response.ReasonPhrase}\n{json}", null, response.StatusCode);

        return json;
    }

    private static bool HasValidToken() =>
        !string.IsNullOrWhiteSpace(CachedToken) && DateTime.UtcNow < TokenExpiresAtUtc;

    private static void ClearToken()
    {
        CachedToken = null;
        TokenExpiresAtUtc = DateTime.MinValue;
    }

    private static string? ExtractToken(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return null;

        json = json.Trim();
        if (json.StartsWith('"') && json.EndsWith('"'))
            return JsonSerializer.Deserialize<string>(json);

        if (!json.StartsWith('{'))
            return json;

        using var document = JsonDocument.Parse(json);
        return FindToken(document.RootElement);
    }

    private static string? FindToken(JsonElement element)
    {
        if (element.ValueKind == JsonValueKind.String)
            return element.GetString();

        if (element.ValueKind != JsonValueKind.Object)
            return null;

        foreach (var property in element.EnumerateObject())
        {
            if (property.Value.ValueKind == JsonValueKind.String &&
                (property.Name.Equals("token", StringComparison.OrdinalIgnoreCase) ||
                 property.Name.Equals("accessToken", StringComparison.OrdinalIgnoreCase) ||
                 property.Name.Equals("access_token", StringComparison.OrdinalIgnoreCase) ||
                 property.Name.Equals("jwt", StringComparison.OrdinalIgnoreCase)))
            {
                return property.Value.GetString();
            }
        }

        if (element.TryGetProperty("result", out var result))
            return FindToken(result);

        if (element.TryGetProperty("data", out var data))
            return FindToken(data);

        return null;
    }
}
