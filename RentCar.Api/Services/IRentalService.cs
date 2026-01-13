using RentCar.Api.DTOs;

namespace RentCar.Api.Services
{
    public interface IRentalService
    {
        
        Task<List<RentalHistoryResponse>> GetCustomerRentalHistory(string customerId);
    }
}