using Microsoft.AspNetCore.Mvc;

namespace RentCar.WebUI.Controllers
{
    public class AccountController : Controller
    {

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}