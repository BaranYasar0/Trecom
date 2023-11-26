namespace Trecom.Api.Services.Catalog.Models.Dtos;

public record QuerySearchCriteriasDto(string? Name, string? Description, string? CategoryName, string? BrandName, string? ModelName, string? ColorName, string? SizeName, string? GenderName, string? ProductCode);