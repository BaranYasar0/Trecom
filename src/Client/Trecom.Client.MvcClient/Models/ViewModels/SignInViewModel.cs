namespace Trecom.Client.MvcClient.Models.ViewModels;

public record SignInViewModel
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string AccessToken { get; set; }
}