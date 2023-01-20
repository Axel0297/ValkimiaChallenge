using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Billing.Application.Data.Models
{
    public class InvoiceModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime? Date { get; set; }

        [Required(ErrorMessage = "El detalle es obligatorio")]
        [MaxLength(200)]
        public string? Detail { get; set; }

        [Required(ErrorMessage = "El monto es obligatorio")]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "La factura debe pertenecer a un cliente")]
        public Guid CustomerId { get; set; }

        public CustomerModel? Customer { get; set; }
    }
}
