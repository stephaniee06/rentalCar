using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Api.Models
{
    [Table("LkPayment")]
    public class LkPayment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required]
        public int RentalId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime? PaymentDate { get; set; }

        [StringLength(50)]
        public string Method { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

       
    }
}