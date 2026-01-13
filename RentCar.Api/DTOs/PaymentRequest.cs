namespace RentCar.Api.DTOs
{
    public class PaymentRequest
    {
        public int RentalId { get; set; } 
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}