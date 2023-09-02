using Microsoft.AspNetCore.Mvc;
using Trecom.Client.MvcClient.Models.InputModels;
using Trecom.Client.MvcClient.Services.Interfaces;

namespace Trecom.Client.MvcClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpInputModel model)
        {
            var result= await _authService.SignUpAsync(model);
            
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignIn()
        {
            return View();
        }
    }
}
