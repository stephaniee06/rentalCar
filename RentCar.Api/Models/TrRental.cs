using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Api.Models
{
    [Table("TrRental")]
    public class TrRental
    {
        [Key]
        public int RentalId { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(10)]
        public string CarId { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; }

        [StringLength(20)]
        public string PaymentStatus { get; set; }

 

    
        [ForeignKey("CarId")]
        public virtual MsCar Car { get; set; }

        
    }
}