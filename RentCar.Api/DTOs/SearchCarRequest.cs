namespace RentCar.Api.DTOs
{
    public class SearchCarRequest
    {
        public string? Brand { get; set; }
        public string? Keyword { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? AvailabilityStatus { get; set; }
        public int? Year { get; set; } // Tambahkan ini
        
    }
}