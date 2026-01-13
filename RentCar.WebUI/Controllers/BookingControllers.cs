using Microsoft.AspNetCore.Mvc; // WAJIB ADA: Ini yang memperbaiki error CS0246

namespace RentCar.WebUI.Controllers
{
    public class BookingController : Controller
    {
       
        public IActionResult Riwayat()
        {
            return View();
        }
    }
}