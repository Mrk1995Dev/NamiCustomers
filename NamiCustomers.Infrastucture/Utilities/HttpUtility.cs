using Microsoft.AspNetCore.Http;
using NamiCustomers.Abstractions.Dtos;
using System.Net.Http.Json;

namespace NamiCustomers.Infrastucture.Utilities;

public static class HttpUtility
{
    public static string GetClaimValue(this IHttpContextAccessor httpContextAccessor, MyClaims claimType)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType.ToString())?.Value;
    }

    public static string Message<T>(this HttpResponseMessage httpresponse) where T : class
    {
        return httpresponse?.Content?.ReadFromJsonAsync<ResultDto<T>>()?.Result?.Message;
    }
}
