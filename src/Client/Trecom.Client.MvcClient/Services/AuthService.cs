using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Trecom.Client.MvcClient.Models.InputModels;
using Trecom.Client.MvcClient.Models.ViewModels;
using Trecom.Client.MvcClient.Services.Interfaces;
using Trecom.Shared.Models;

namespace Trecom.Client.MvcClient.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient httpClient;
    private readonly IHttpContextAccessor httpContextAccessor;

    public AuthService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
    {
        this.httpClient = httpClient;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task<RegisteredViewModel?> SignUpAsync(SignUpInputModel signUpInputModel)
    {
        var response = await httpClient.PostAsJsonAsync("Register", signUpInputModel);

        if (!response.IsSuccessStatusCode || response == null)
        {
            Console.WriteLine($"{response.RequestMessage}");
        }
        RegisteredViewModel? registeredViewModel = await response!.Content?.ReadFromJsonAsync<RegisteredViewModel>();
        return registeredViewModel;
    }

    public async Task<BaseViewModel<SignInViewModel?>> SignInAsync(SignInInputModel signInInputModel)
    {
        var response = await httpClient.PostAsJsonAsync("Login", signInInputModel);

        if (!response.IsSuccessStatusCode || response == null)
        {
            Console.WriteLine($"{response.RequestMessage}");
        }

        ApiResponse<SignInViewModel> result = await response?.Content?.ReadFromJsonAsync<ApiResponse<SignInViewModel>>();

        if (!result.IsSuccess)
        {
            return new BaseViewModel<SignInViewModel?>
            {
                Errors = result.Errors
            };
        }

        await CreateCookie(signInInputModel, result);

        return BaseViewModel<SignInViewModel?>.Create(result.Data);
    }

    private async Task CreateCookie(SignInInputModel signInInputModel, ApiResponse<SignInViewModel> result)
    {
        JwtSecurityToken jwt = new JwtSecurityTokenHandler().ReadJwtToken(result.Data.AccessToken);

        ClaimsIdentity claimsIdentity = new(
            new Claim[]
                { new Claim(ClaimTypes.Name, result.Data.FullName) });

        ClaimsIdentity clm = new(jwt.Claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal claimsPrincipal = new(clm);

        AuthenticationProperties authenticationProperties = new()
        {
            IsPersistent = signInInputModel.IsRemember,
            ExpiresUtc = DateTime.UtcNow.AddDays(7)
        };

        await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal, authenticationProperties);
    }

    public async Task SignOutAsync()
    {
        await httpContextAccessor.HttpContext.SignOutAsync();
    }
}