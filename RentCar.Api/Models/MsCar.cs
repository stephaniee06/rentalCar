using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; // Dibutuhkan untuk ICollection

namespace RentCar.Api.Models
{
    public class MsCar
    {
        [Key]
        public string CarId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(200)]
        public string Brand { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public int Year { get; set; }

        public decimal PricePerDay { get; set; }

        [StringLength(2000)]
        public string Status { get; set; }

        public string? AvailabilityStatus { get; set; }

        
        public virtual ICollection<MsCarImages> MsCarImages { get; set; } = new List<MsCarImages>();

    }
}