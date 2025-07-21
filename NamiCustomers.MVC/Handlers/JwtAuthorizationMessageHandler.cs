using NamiCustomers.MVC.Services.Auth;
using System.Net.Http.Headers;

namespace NamiCustomers.MVC.Handlers;

public class JwtAuthorizationMessageHandler(ITokenSessionService tokenSessionService,
    IAuthService authService) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var token = tokenSessionService.GetAuthToken();

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            //todo ali if token was null must be ?

            var response = await base.SendAsync(request, cancellationToken);


            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                //send RefreshToken
                var refreshToken = tokenSessionService.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var newToken = await authService.RefreshTokenAsync(refreshToken);
                    if (!string.IsNullOrEmpty(newToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                        return await base.SendAsync(request, cancellationToken);
                    }
                }
                return response;
            }


            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in JwtAuthorizationMessageHandler: {ex.Message}");
            throw;

        }

    }
}
