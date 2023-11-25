namespace Trecom.Client.MvcClient.Models.ViewModels;

public record CategoryViewModel(
    Guid Id,
    List<string> Names,
    string? Icon);