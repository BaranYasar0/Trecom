using Trecom.Client.MvcClient.Models.InputModels;
using Trecom.Client.MvcClient.Models.ViewModels;

namespace Trecom.Client.MvcClient.Services.Interfaces;

public interface IAuthService
{
    Task<RegisteredViewModel?> SignUpAsync(SignUpInputModel signUpInputModel);
    Task<BaseViewModel<SignInViewModel?>> SignInAsync(SignInInputModel signInInputModel);
    Task SignOutAsync();
}