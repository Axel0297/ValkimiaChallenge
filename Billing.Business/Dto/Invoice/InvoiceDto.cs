
namespace Billing.Business.Dto.Invoice
{
    public class InvoiceDto
    {
        public Guid? Id { get; set; }

        public DateTime? Date { get; set; }

        public string? Detail { get; set; }

        public decimal? Amount { get; set; }
    }
}
