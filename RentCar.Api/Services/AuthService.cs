using RentCar.Api.Data;   
using RentCar.Api.Models;  
using RentCar.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace RentCar.Api.Services
{
    public class AuthService
    {
        private readonly RentCarDbContext _context;

      
        public AuthService(RentCarDbContext context)
        {
            _context = context;
        }

    
        public async Task<bool> IsEmailRegistered(string email)
        {
            return await _context.MsCustomers.AnyAsync(x => x.Email == email);
        }
    }
}