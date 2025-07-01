using Microsoft.AspNetCore.Http;

namespace NamiCustomers.Infrastucture.Utilities;

public static class HttpUtility
{
    public static string GetClaimValue(this IHttpContextAccessor httpContextAccessor, MyClaims claimType)
    {
        return httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == claimType.ToString())?.Value;
    }
}
