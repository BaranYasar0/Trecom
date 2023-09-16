namespace Trecom.Client.MvcClient.Models.InputModels;

public record SignInInputModel(
    string Email,
    string Password,
    bool IsRemember);