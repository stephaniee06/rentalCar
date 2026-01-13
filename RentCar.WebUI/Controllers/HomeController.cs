using Microsoft.AspNetCore.Mvc;

namespace RentCar.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
