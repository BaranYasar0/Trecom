using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult Account()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult EditProfile()
        {
            return View();
        }

    }
}
