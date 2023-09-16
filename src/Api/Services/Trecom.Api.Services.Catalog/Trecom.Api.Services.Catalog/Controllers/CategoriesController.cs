using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.Repository;

namespace Trecom.Api.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly CategoryRepository categoryRepository;
    private readonly IMapper mapper;
    public CategoriesController(CategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] QueryPaginationDto pagination)
    {
        var query = new GetCategoryListQuery(pagination);
        var result = await MediatR.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateCategoryDto categoryDto)
    {
        Category category=mapper.Map<Category>(categoryDto);

        CategoryResponseDto response=await categoryRepository.CreateCategoryAsync(category);

        return Ok(response);
    }
}