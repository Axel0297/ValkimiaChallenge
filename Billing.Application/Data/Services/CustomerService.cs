using Billing.Application.Data.Models;
using Billing.Application.Data.Services.Contracts;
using Billing.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Text;

namespace Billing.Application.Data.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly string _urlBase = "https://localhost:7158";

        private readonly HttpClient _http;
        private readonly ITokenService _tokenService;
        private readonly CustomAuthenticationStateProvider _authenticationStateProvider;

        public CustomerService(HttpClient http,
                               ITokenService tokenService,
                               CustomAuthenticationStateProvider authenticationStateProvider)
        {
            _http = http;
            _tokenService = tokenService;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<TokenModel?> LoginAsync(CustomerLoginModel customerLogin)
        {
            string url = _urlBase + "/Customer/login";
            using HttpClient client = new();

            StringContent content = new(JsonConvert.SerializeObject(customerLogin), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(url, content);

            string apiResponse = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TokenModel>(apiResponse);

            if (result != null)
            {
                await _tokenService.SetToken(result);
                _authenticationStateProvider.StateChanged();
            }

            return result;
        }

        public async Task LogoutAsync()
        {
            await _tokenService.RemoveToken();
            _authenticationStateProvider.StateChanged();
        }

        public async Task<CustomerModel> GetCustomerAsync(Guid id)
        {
            string url = _urlBase + $"/Customer?id={id}";

            HttpResponseMessage responseMessage = await _http.GetAsync(url);
            string apiResponse = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerModel>(apiResponse);
        }

        public async Task<List<CustomerListModel>> GetAllCustomersAsync()
        {
            List<CustomerListModel> customers = new();

            string url = _urlBase + "/Customer/list";
            HttpResponseMessage responseMessage = await _http.GetAsync(url);
            string apiResponse = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(apiResponse)) return customers;

            customers = JsonConvert.DeserializeObject<List<CustomerModel>?>(apiResponse).Select(
                s => new CustomerListModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    LastName = s.LastName,
                    Address = s.Address,
                    Email = s.Email,
                    Enabled = s.Enabled,
                }
                ).ToList();

            return customers;
        }

        public async Task<bool> CreateCustomerAsync(CustomerModel customer)
        {
            string url = _urlBase + "/Customer";

            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _http.PostAsync(url, content);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customer)
        {
            string url = _urlBase + "/Customer";

            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _http.PutAsync(url, content);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            string url = _urlBase + $"/Customer?id={id}";

            HttpResponseMessage responseMessage = await _http.DeleteAsync(url);

        }
    }
}
