using System.ComponentModel.DataAnnotations;

namespace RentCar.Api.Models
{
    public class MsCustomer
    {
        [Key]
        public string CustomerId { get; set; } = Guid.NewGuid().ToString().Substring(0, 10);
        
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        
      
        public string PasswordHash { get; set; } 
    }
}