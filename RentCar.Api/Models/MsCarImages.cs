using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Api.Models
{
    [Table("MsCarImages")]
    public class MsCarImages
    {
        [Key]
        public int Image_car_id { get; set; }

        [Required]
         
        public string Car_id { get; set; } 

        [Required]
        [StringLength(255)]
        public string image_link { get; set; }

        
        [ForeignKey("Car_id")]
        public virtual MsCar MsCar { get; set; }
    }
}