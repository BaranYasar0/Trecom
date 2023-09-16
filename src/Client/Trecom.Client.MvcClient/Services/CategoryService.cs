using Newtonsoft.Json;
using Trecom.Client.MvcClient.Models.ViewModels;
using Trecom.Client.MvcClient.Services.Interfaces;
using Trecom.Shared.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Trecom.Client.MvcClient.Services
{
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

            var result= await client.GetAsync("categories?PageSize=50&Page=1");

            var data=await result.Content.ReadAsStringAsync();
            ApiResponse<List<CategoryViewModel>> response = await client.GetFromJsonAsync<ApiResponse<List<CategoryViewModel>>>("categories");


            if(!response.IsSuccess)
                return Enumerable.Empty<CategoryViewModel>().ToList();

            return response.Data;
        }
    }
}
