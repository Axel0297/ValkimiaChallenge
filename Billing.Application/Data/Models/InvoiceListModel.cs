using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Billing.Application.Data.Models
{
    public class InvoiceListModel
    {
        public Guid? Id { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        [MaxLength(200)]
        public string? Detail { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal? Amount { get; set; }
    }
}
