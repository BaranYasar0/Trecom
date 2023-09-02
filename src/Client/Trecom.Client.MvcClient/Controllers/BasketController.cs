using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Controllers
{
    public class BasketController : Controller
    {
        public IActionResult GetBasket()
        {
            return View();
        }
    }
}
