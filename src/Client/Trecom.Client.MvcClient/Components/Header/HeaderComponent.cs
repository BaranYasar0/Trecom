using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Components.Header;

public class HeaderComponent:ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}