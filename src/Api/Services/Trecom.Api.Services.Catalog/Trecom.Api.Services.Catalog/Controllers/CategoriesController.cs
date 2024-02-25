using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Trecom.Api.Services.Catalog.Application.Features.Queries;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;

namespace Trecom.Api.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : BaseController
{
    private readonly CategoryElasticRepository categoryRepository;
    private readonly IMapper mapper;
    public CategoriesController(CategoryElasticRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories([FromQuery] QueryPaginationDto pagination)
    {
        var test1= new Test("MOBILE", 1);
        var test2= new Test("PRIVATE", 1);
        var test3= new Test("BRANCH", 1);
        var test4= new Test("PRIVATE", 2);
        var test5= new Test("BRANCH", 2);
        var testList = new List<Test>() { test1, test2, test3, test4, test5};
        var testList2 = testList.GroupBy(x => x.Id);
        foreach (var test in testList2)
        {
            
        }
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

public class Test
{
    public string Channel { get; set; }
    public int Id { get; set; }

    public Test(string channel, int id)
    {
        Channel = channel;
        Id = id;
    }
}