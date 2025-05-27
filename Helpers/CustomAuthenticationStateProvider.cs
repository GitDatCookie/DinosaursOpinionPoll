using AI_Project.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AI_Project.Helpers
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private AuthenticationState authenticationState;

        public CustomAuthenticationStateProvider(CustomAuthenticationService service)
        {
            authenticationState = new AuthenticationState(service.CurrentUser);

            service.UserChanged += (newUser) =>
            {
                authenticationState = new AuthenticationState(newUser);
                NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
            };
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
            Task.FromResult(authenticationState);


    }
}
