using Billing.Application.Data.Models;
using Billing.Application.Data.Services.Contracts;
using Newtonsoft.Json;

namespace Billing.Application.Data.Services
{
    public class CityService : ICityService
    {
        private readonly string _urlBase = "https://localhost:7158";

        private readonly HttpClient _http;

        public CityService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CityModel>> GetAllCitiesAsync()
        {
            List<CityModel> cities = new();

            string url = _urlBase + "/City";
            HttpResponseMessage responseMessage = await _http.GetAsync(url);
            string apiResponse = await responseMessage.Content.ReadAsStringAsync();
            cities = JsonConvert.DeserializeObject<List<CityModel>?>(apiResponse).Select(
                s => new CityModel
                {
                    Id = s.Id,
                    Name = s.Name,
                }
                ).ToList();

            return cities;
        }
    }
}
