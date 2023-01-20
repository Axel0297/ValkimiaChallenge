using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Billing.Data.Entities
{
    [Table("Customer")]
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Nombre")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Apellido")]
        public string? Lastname { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("Domicilio")]
        public string? Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Password { get; set; }

        [Column("Habilitado")]
        public bool Enabled { get; set; } = true;

        [Column("IdCiudad")]
        [ForeignKey("City")]
        public Guid CityId { get; set; }

        public City? City { get; set; }

        public List<Invoice>? Invoices { get; set; }
    }
}
