namespace Trecom.Api.Services.Catalog.Models.Dtos;

public class CategoryResponseDto
{
    public Guid Id { get; set; }
    public List<string> Names { get; set; }

    public CategoryResponseDto(Guid id, List<string> names)
    {
        Id = id;
        Names = names;
    }
}