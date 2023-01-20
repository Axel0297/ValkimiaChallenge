
namespace Billing.Business.Dto.Invoice
{
    public class InvoicePayloadDto
    {
        public Guid? Id { get; set; }

        public DateTime? Date { get; set; }

        public string? Detail { get; set; }

        public decimal? Amount { get; set; }

        public Guid CustomerId { get; set; }
    }
}
