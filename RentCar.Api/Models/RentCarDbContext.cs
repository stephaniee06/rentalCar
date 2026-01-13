using Microsoft.EntityFrameworkCore;
using RentCar.Api.Models;

namespace RentCar.Api.Models
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options)
            : base(options)
        {
        }

        // Mendaftarkan semua model sebagai DbSet (Tabel)
        public DbSet<MsCar> MsCars { get; set; } 
        public DbSet<MsCustomer> MsCustomers { get; set; } 
        public DbSet<MsCarImages> MsCarImages { get; set; } 
        public DbSet<TrRental> TrRentals { get; set; } 
        public DbSet<TrMaintenance> TrMaintenances { get; set; } 
        public DbSet<LkPayment> LkPayments { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<MsCarImages>()
                .HasKey(c => c.Image_car_id); 
 
            modelBuilder.Entity<TrMaintenance>()
                .Property(m => m.Cost)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<TrRental>()
                .Property(r => r.TotalPrice)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<LkPayment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(18,2)"); 

            modelBuilder.Entity<MsCustomer>().ToTable("MsCustomer");

            modelBuilder.Entity<MsCar>().ToTable("MsCar");
        }
    }
}