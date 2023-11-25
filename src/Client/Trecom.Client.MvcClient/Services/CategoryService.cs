using Newtonsoft.Json;
using Trecom.Client.MvcClient.Models.ViewModels;
using Trecom.Client.MvcClient.Services.Interfaces;
using Trecom.Shared.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Trecom.Client.MvcClient.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory httpClientFactory;

    public CategoryService(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;
    }

    public async Task<List<CategoryViewModel>> GetCategoriesAsync()
    {
        HttpClient client = httpClientFactory.CreateClient("category");

        var result = await client.GetAsync("categories?PageSize=50&Page=1");
        result.EnsureSuccessStatusCode();
        var response = await result.Content.ReadFromJsonAsync<ApiResponse<PaginationViewModel<CategoryViewModel>>>();

        if (!response.ValidateSuccess())
            return Enumerable.Empty<CategoryViewModel>().ToList();

        response.Data.Items = response.Data.Items.OrderBy(x => x.Names[1]).ToList();

        return response.Data.Items;
    }

    private List<CategoryViewModel> ConfigureCategoriesForOrder(List<CategoryViewModel> categories)
    {
        Func<CategoryViewModel, string> orderCateries = (CategoryViewModel categories) => categories.Names.FirstOrDefault();

        return categories.OrderBy(orderCateries).ToList();
    }
}