using System;

namespace RentCar.Api.DTOs
{
    public class RentalRequest
    {
     
        public string CustomerId { get; set; }

        
        public string CarId { get; set; }

       
        public DateTime RentalDate { get; set; }

        
        public DateTime ReturnDate { get; set; }
        
      
        public decimal? TotalPrice { get; set; }
    }
}