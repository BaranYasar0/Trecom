using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Components.User;

public class UserDashboardSidebarProfileComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}