using NamiCustomers.Web.Services.Auth.AuthServices;
using NamiCustomers.Web.Services.Auth.TokenServices;
using System.Net.Http.Headers;

namespace NamiCustomers.Web.Handlers
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ITokenService tokenService;
        private readonly IAuthService authService;
        private readonly NavigationManager navigationManager;
        public JwtAuthorizationMessageHandler(ITokenService tokenService,
            IAuthService authService,
            NavigationManager navigationManager)
        {
            this.tokenService = tokenService;
            this.authService = authService;
            this.navigationManager = navigationManager;

        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await tokenService.GetAuthTokenAsync();

                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                var response = await base.SendAsync(request, cancellationToken);


                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //send RefreshToken
                    var refreshToken = await tokenService.GetRefreshTokenAsync();
                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        var newToken = await authService.RefreshTokenAsync(refreshToken);
                        if (!string.IsNullOrEmpty(newToken))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                            return await base.SendAsync(request, cancellationToken);
                        }
                    }

                    navigationManager.NavigateTo("login", true);
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
}
