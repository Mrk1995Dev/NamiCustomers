using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NamiCustomers.MVC.Services.Auth
{
    public class CustomAuthenticationStateProvider : ServerAuthenticationStateProvider//AuthenticationStateProvider
    {
        private readonly ITokenSessionService tokenService;
        private readonly NavigationManager _navigationManager;
        public CustomAuthenticationStateProvider(ITokenSessionService tokenService, NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            this.tokenService = tokenService;

        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = tokenService.GetAuthToken();

            if (string.IsNullOrEmpty(token))
            {
                //_navigationManager.NavigateTo("/Account/LoginByMobile", forceLoad: true);
                //کاربر لاگین نیست
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claimsPrincipal = CreateClaimsPrincipalFromJwt(token);
            return new AuthenticationState(claimsPrincipal);

        }

        private ClaimsPrincipal CreateClaimsPrincipalFromJwt(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            if (!tokenHandler.CanReadToken(jwt))
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var jwtToken = tokenHandler.ReadJwtToken(jwt);

            var claims = jwtToken.Claims;

            var identity = new ClaimsIdentity(claims, "jwt");

            return new ClaimsPrincipal(identity);
        }


        public void UpdateAuthenticationState()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

    }
}
