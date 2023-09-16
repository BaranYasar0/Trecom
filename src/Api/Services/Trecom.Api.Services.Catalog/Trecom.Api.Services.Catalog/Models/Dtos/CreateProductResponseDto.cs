namespace Trecom.Api.Services.Catalog.Models.Dtos;

public class CreateProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
}