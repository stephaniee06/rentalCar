using Microsoft.AspNetCore.Mvc;
using RentCar.Api.Services;
using RentCar.Api.Models;
using RentCar.Api.DTOs;

 
namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            try
            {
                var cars = await _carService.GetAllCarsAsync();
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> SearchCars([FromBody] SearchCarRequest request)
        {
            try
            {
                var results = await _carService.SearchCarsAsync(request);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}