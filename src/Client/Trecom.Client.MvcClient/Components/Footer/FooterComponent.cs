using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Components.Footer;

public class FooterComponent:ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View();
    }
}