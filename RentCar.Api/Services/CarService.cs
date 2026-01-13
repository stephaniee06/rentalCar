using Microsoft.EntityFrameworkCore;
using RentCar.Api.Models;  
using RentCar.Api.DTOs;

namespace RentCar.Api.Services
{
    public class CarService
    {
        private readonly RentCarDbContext _context;

        public CarService(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<MsCar>> GetAllCarsAsync()
        {
            return await _context.MsCars
                .Include(c => c.MsCarImages) 
                .ToListAsync();
        }

        public async Task<List<MsCar>> SearchCarsAsync(SearchCarRequest request)
{
    var query = _context.MsCars.Include(c => c.MsCarImages).AsQueryable();

    if (request != null)
    {
         
        if (!string.IsNullOrEmpty(request.Brand))
            query = query.Where(c => c.Brand.Contains(request.Brand));
        
        
        if (!string.IsNullOrEmpty(request.Keyword))
            query = query.Where(c => c.Brand.Contains(request.Keyword) || c.Model.Contains(request.Keyword));
 
        if (request.Year.HasValue && request.Year > 0)
            query = query.Where(c => c.Year == request.Year);

      
        if (request.MinPrice > 0)
            query = query.Where(c => c.PricePerDay >= request.MinPrice);

        if (request.MaxPrice > 0)
            query = query.Where(c => c.PricePerDay <= request.MaxPrice);

       
        if (!string.IsNullOrEmpty(request.AvailabilityStatus) && request.AvailabilityStatus != "string")
            query = query.Where(c => c.AvailabilityStatus == request.AvailabilityStatus);

        
    }

    return await query.ToListAsync();
}
    }
}