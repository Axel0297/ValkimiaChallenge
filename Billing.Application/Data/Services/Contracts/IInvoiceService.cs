using Billing.Application.Data.Models;

namespace Billing.Application.Data.Services.Contracts
{
    public interface IInvoiceService
    {
        Task<bool> CreateInvoiceAsync(InvoiceModel invoice);

        Task<InvoiceModel> GetInvoiceAsync(Guid id);

        Task<List<InvoiceListModel>> GetInvoicesByCustomerId(Guid customerId);
    }
}
