using Microsoft.AspNetCore.Mvc;

namespace Trecom.Client.MvcClient.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult GetAllProducts()
        {
            return View();
        }
    }
}
