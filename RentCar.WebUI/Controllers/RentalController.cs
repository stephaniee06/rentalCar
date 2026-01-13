using Microsoft.AspNetCore.Mvc;

namespace RentCar.Web.Controllers
{
    public class RentalController : Controller
    {
        public IActionResult Checkout()
        {
            
            return View("~/Views/Rental/Checkout.cshtml");
        }

        public IActionResult History()
        {
            return View();
        }
    }
}