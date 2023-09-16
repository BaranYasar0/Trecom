using Trecom.Client.MvcClient.Models.ViewModels;

namespace Trecom.Client.MvcClient.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetCategoriesAsync();
    }
}
