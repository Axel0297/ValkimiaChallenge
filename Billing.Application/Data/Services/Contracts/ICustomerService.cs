using Billing.Application.Data.Models;

namespace Billing.Application.Data.Services.Contracts
{
    public interface ICustomerService
    {
        Task<TokenModel?> LoginAsync(CustomerLoginModel customerLogin);

        Task LogoutAsync();

        Task<CustomerModel> GetCustomerAsync(Guid id);

        Task<List<CustomerListModel>> GetAllCustomersAsync();

        Task<bool> CreateCustomerAsync(CustomerModel customer);

        Task<bool> UpdateCustomerAsync(CustomerModel customer);

        Task DeleteCustomerAsync(Guid id);
    }
}
