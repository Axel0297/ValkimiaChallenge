using Billing.Application.Data.Models;

namespace Billing.Application.Data.Services.Contracts
{
    public interface ICityService
    {
        Task<List<CityModel>> GetAllCitiesAsync();
    }
}
