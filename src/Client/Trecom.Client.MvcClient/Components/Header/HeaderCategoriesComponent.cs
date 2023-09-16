using Microsoft.AspNetCore.Mvc;
using Trecom.Client.MvcClient.Models.ViewModels;
using Trecom.Client.MvcClient.Services.Interfaces;

namespace Trecom.Client.MvcClient.Components.Header;

public class HeaderCategoriesComponent:ViewComponent
{
    private readonly ICategoryService categoryService;

    public HeaderCategoriesComponent(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<CategoryViewModel> categories = await categoryService.GetCategoriesAsync();
        return View(categories);
    }
}