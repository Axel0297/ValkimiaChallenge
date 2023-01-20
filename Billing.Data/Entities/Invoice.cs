using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Data.Entities
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime? Date { get; set; }

        [Required]
        [MaxLength(200)]
        [Column("Detalle")]
        public string? Detail { get; set; }

        [Required]
        [Column("Importe", TypeName = "decimal(18,4)")]
        public decimal? Amount { get; set; }

        [Column("IdCliente")]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
