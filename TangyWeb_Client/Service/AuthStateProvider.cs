using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using Tangy_Common;
using TangyWeb_Client.Helper;

namespace TangyWeb_Client.Service
{
  public class AuthStateProvider:AuthenticationStateProvider
  {
    private readonly HttpClient _httpClient;
    private readonly ILocalStorageService _localstorage;

    public AuthStateProvider(HttpClient httpClient,ILocalStorageService localStorage)
    {
      _httpClient = httpClient;
      _localstorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
      var token = await _localstorage.GetItemAsync<string>(StaticDetails.Local_Token);
      if (token == null)
      {
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
      }

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",token);

      return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),"jwtAuthType")));
    }

    public void NotifyUserLoggedIn(string token)
    {
      var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));
      var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
      NotifyAuthenticationStateChanged(authState);
    }
    public void NotifyUserlogout()
    {
      var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
      NotifyAuthenticationStateChanged(authState);
    }
  }
}
