using Billing.Application.Data.Models;
using Billing.Application.Data.Services.Contracts;
using Billing.Application.Services;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace Billing.Application.Data.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly string _urlBase = "https://localhost:7158";

        private readonly HttpClient _http;
        private readonly ITokenService _tokenService;

        public InvoiceService(HttpClient http, ITokenService tokenService)
        {
            _http = http;
            _tokenService = tokenService;
        }

        public async Task<bool> CreateInvoiceAsync(InvoiceModel invoice)
        {
            string url = _urlBase + "/Invoice";

            StringContent content = new(JsonConvert.SerializeObject(invoice), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _http.PostAsync(url, content);
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<InvoiceModel> GetInvoiceAsync(Guid id)
        {
            string url = _urlBase + $"/Invoice?id={id}";

            var token = await _tokenService.GetToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            HttpResponseMessage responseMessage = await _http.GetAsync(url);
            string apiResponse = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<InvoiceModel>(apiResponse);
        }

        public async Task<List<InvoiceListModel>> GetInvoicesByCustomerId(Guid customerId)
        {
            string url = _urlBase + $"/Invoice/list?customerId={customerId}";

            var token = await _tokenService.GetToken();
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            List<InvoiceListModel> invoices = new();

            HttpResponseMessage responseMessage = await _http.GetAsync(url);
            string apiResponse = await responseMessage.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(apiResponse)) return invoices;

            invoices = JsonConvert.DeserializeObject<List<InvoiceListModel>>(apiResponse).Select(
                s => new InvoiceListModel
                {
                    Id = s.Id,
                    Date = s.Date,
                    Detail = s.Detail,
                    Amount = s.Amount
                }
                ).ToList();

            return invoices;
        }
    }
}
