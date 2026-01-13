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
    public class AuthController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public AuthController(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request) 
        {
            try 
            {
                if (string.IsNullOrEmpty(request.Email)) 
                    return BadRequest(new { message = "Email wajib diisi!" });

                 
                var exists = await _context.MsCustomers.AnyAsync(u => u.Email == request.Email);
                if (exists) return BadRequest(new { message = "Email sudah terdaftar!" });

                var newCustomer = new MsCustomer
                {
                    CustomerId = string.IsNullOrEmpty(request.CustomerId) 
                                 ? Guid.NewGuid().ToString().Substring(0, 10) 
                                 : request.CustomerId,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    Address = request.Address,
                    PasswordHash = request.Password 
                };

                _context.MsCustomers.Add(newCustomer);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Registrasi Berhasil!" });
            }
            catch (Exception ex)
            {
                var innerMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return BadRequest(new { message = "Gagal menyimpan: " + innerMessage });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
             
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                return BadRequest(new { message = "Email dan Password wajib diisi" });

          
            var user = await _context.MsCustomers
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Username.ToLower() 
                                       && u.PasswordHash == request.Password);

            if (user == null) 
                return Unauthorized(new { message = "Email atau Password Salah" });

           return Ok(new { 
        message = "Login Berhasil", 
        name = user.Name,   
        email = user.Email
    });
        }
    }
}