using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Api.Models
{
    [Table("TrMaintenance")]
    public class TrMaintenance
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required]
        [StringLength(10)]
        public string CarId { get; set; }

        [Required]
        [StringLength(10)]
        public string EmployeeId { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Cost { get; set; }

        public DateTime? DateService { get; set; }

      
    }
}