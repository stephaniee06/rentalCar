using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Api.Data;
using RentCar.Api.Models;
using RentCar.Api.DTOs;
using RentCar.Api.Models;


namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public PaymentController(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                 
                var rental = await _context.TrRentals.FindAsync(request.RentalId);
                if (rental == null)
                {
                    return NotFound(new { message = "Data rental tidak ditemukan" });
                }

                 
                var newPayment = new LkPayment
                {
                    RentalId = request.RentalId,
                    Amount = request.Amount,
                    PaymentDate = DateTime.Now,
                    Method = request.Method,
                    Status = "Success"
                };

                _context.LkPayments.Add(newPayment);

               
                rental.PaymentStatus = "Sudah Dibayar";

                 
                await _context.SaveChangesAsync();
                
                 
                await transaction.CommitAsync();

                return Ok(new { message = "Pembayaran berhasil diproses!" });
            }
            catch (Exception ex)
            {
              
                await transaction.RollbackAsync();
                return BadRequest(new { message = "Terjadi kesalahan: " + ex.Message });
            }
        }
    }
}