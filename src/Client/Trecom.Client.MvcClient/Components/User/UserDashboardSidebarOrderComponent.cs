using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Components.User;

public class UserDashboardSidebarOrderComponent:ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}