namespace Trecom.Client.MvcClient.Services;

public record SignInInputModel(
    string Email,
    string Password,
    bool IsRemember);