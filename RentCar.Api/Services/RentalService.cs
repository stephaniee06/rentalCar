using Microsoft.EntityFrameworkCore;
using RentCar.Api.Models;
using RentCar.Api.DTOs;

namespace RentCar.Api.Services
{
    public class RentalService : IRentalService
    {
        private readonly RentCarDbContext _context;

        public RentalService(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<RentalHistoryResponse>> GetCustomerRentalHistory(string customerId)
        {
            return await _context.TrRentals
                .Include(r => r.Car) 
                .Where(r => r.CustomerId == customerId)
                .OrderByDescending(r => r.RentalDate)
                .Select(r => new RentalHistoryResponse
                {
                    RentalDateRange = $"{r.RentalDate:dd MMMM yyyy} - {r.ReturnDate:dd MMMM yyyy}",
                    CarName = $"{r.Car.Brand} {r.Car.Model} ({r.Car.Year})",
                    PricePerDay = r.Car.PricePerDay,
                    TotalDays = (r.ReturnDate - r.RentalDate).Days,
                    TotalPrice = r.TotalPrice ?? 0,
                    PaymentStatus = r.PaymentStatus ?? "Belum Dibayar"
                })
                .ToListAsync();
        }
    }
}