using Microsoft.EntityFrameworkCore;
using RentCar.Api.Models;

namespace RentCar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MsCar> MsCars { get; set; }  
        public DbSet<MsCustomer> MsCustomers { get; set; }  
    }
}