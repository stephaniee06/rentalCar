using Microsoft.AspNetCore.Mvc;
using RentCar.Api.Services;
using RentCar.Api.DTOs;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        
        [HttpGet("history/{customerId}")]
        public async Task<IActionResult> GetHistory(string customerId)
        {
            var history = await _rentalService.GetCustomerRentalHistory(customerId);
            
            if (history == null || history.Count == 0)
            {
                return NotFound(new { message = "Data riwayat tidak ditemukan" });
            }

            return Ok(history);
        }
    }
}