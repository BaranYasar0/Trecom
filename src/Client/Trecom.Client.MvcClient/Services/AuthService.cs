using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Client.MvcClient.Models.InputModels;
using Trecom.Client.MvcClient.Models.ViewModels;
using Trecom.Client.MvcClient.Services.Interfaces;

namespace Trecom.Client.MvcClient.Services
{
    public class AuthService:IAuthService
    {
        private readonly HttpClient httpClient;
        
        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<RegisteredViewModel?> SignUpAsync(SignUpInputModel signUpInputModel)
        {
            var response = await httpClient.PostAsJsonAsync("Register", signUpInputModel);

            if (!response.IsSuccessStatusCode || response==null)
            {
                Console.WriteLine($"{response.RequestMessage}");
            }
            RegisteredViewModel? registeredViewModel = await response!.Content?.ReadFromJsonAsync<RegisteredViewModel>();
            return registeredViewModel;
        }

        public async Task<SignInViewModel?> SignInAsync(SignInInputModel signInInputModel)
        {
            var response = await httpClient.PostAsJsonAsync("Login", signInInputModel);

            if (!response.IsSuccessStatusCode || response == null)
            {
                Console.WriteLine($"{response.RequestMessage}");
            }
            SignInViewModel? signInViewModel = await response!.Content?.ReadFromJsonAsync<SignInViewModel>();
            return signInViewModel;
        }
    }
}
